using Timer = System.Windows.Forms.Timer;

namespace AnalogicalWatcher
{
    public partial class Watcher : Form
    {
        private Timer timer;

        public Watcher()
        {
            this.Text = "Relógio Analógico";
            this.Size = new Size(400, 400);
            this.DoubleBuffered = true;

            timer = new Timer();
            timer.Interval = 1000; // Atualiza a cada segundo
            timer.Tick += (s, e) => this.Invalidate(); // Redesenha o relógio
            timer.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int cx = this.ClientSize.Width / 2;
            int cy = this.ClientSize.Height / 2;
            int raio = Math.Min(cx, cy) - 10;

            // Desenha o círculo do relógio
            g.DrawEllipse(Pens.Black, cx - raio, cy - raio, 2 * raio, 2 * raio);

            // Desenha as marcações do relógio
            for (int i = 1; i <= 12; i++)
            {
                double angulo = i * Math.PI / 6;
                int x = cx + (int)((raio * 0.8) * Math.Sin(angulo));
                int y = cy - (int)((raio * 0.8) * Math.Cos(angulo));

                // Adiciona os números
                string numero = i.ToString();
                SizeF tamanho = g.MeasureString(numero, this.Font);
                g.DrawString(numero, this.Font, Brushes.Black, x - tamanho.Width / 2, y - tamanho.Height / 2);
            }

            for (int i = 0; i < 60; i++)
            {
                double angulo = i * Math.PI / 30;
                int x1 = cx + (int)(raio * 0.9 * Math.Sin(angulo));
                int y1 = cy - (int)(raio * 0.9 * Math.Cos(angulo));
                int x2 = cx + (int)(raio * Math.Sin(angulo));
                int y2 = cy - (int)(raio * Math.Cos(angulo));

                Pen pen = (i % 5 == 0) ? Pens.Black : Pens.Gray;
                g.DrawLine(pen, x1, y1, x2, y2);
            }

            // Obtém o horário atual
            DateTime agora = DateTime.Now;
            double anguloHora = (agora.Hour % 12 + agora.Minute / 60.0) * Math.PI / 6;
            double anguloMinuto = (agora.Minute + agora.Second / 60.0) * Math.PI / 30;
            double anguloSegundo = agora.Second * Math.PI / 30;

            // Ponteiro das horas
            DesenhaPonteiro(g, cx, cy, anguloHora, raio * 0.5, Color.Black, 6);

            // Ponteiro dos minutos
            DesenhaPonteiro(g, cx, cy, anguloMinuto, raio * 0.7, Color.Black, 4);

            // Ponteiro dos segundos
            DesenhaPonteiro(g, cx, cy, anguloSegundo, raio * 0.9, Color.Red, 2);
        }

        private void DesenhaPonteiro(Graphics g, int cx, int cy, double angulo, double comprimento, Color cor, int largura)
        {
            int x = cx + (int)(comprimento * Math.Sin(angulo));
            int y = cy - (int)(comprimento * Math.Cos(angulo));

            using (Pen pen = new Pen(cor, largura))
            {
                g.DrawLine(pen, cx, cy, x, y);
            }
        }
    }
}
