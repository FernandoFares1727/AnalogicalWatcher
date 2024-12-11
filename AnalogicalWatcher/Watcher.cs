using Timer = System.Windows.Forms.Timer;

namespace AnalogicalWatcher
{
    public partial class Watcher : Form
    {
        private Timer timer;
        private Color corHora = Color.Black;
        private Color corMinuto = Color.Black;
        private Color corSegundo = Color.Red;
        private Color corFundo = Color.White;

        public Watcher()
        {
            this.Text = "Relógio Analógico";
            this.Size = new Size(400, 400);
            this.DoubleBuffered = true;

            timer = new Timer();
            timer.Interval = 1000; // Atualiza a cada segundo
            timer.Tick += (s, e) => this.Invalidate(); // Redesenha o relógio
            timer.Start();

            this.MouseDown += Watcher_MouseDown;
        }

        private void Watcher_MouseDown(object sender, MouseEventArgs e)
        {
            int cx = this.ClientSize.Width / 2;
            int cy = this.ClientSize.Height / 2;
            int raio = Math.Min(cx, cy) - 10;

            // Calcula distâncias para identificar qual ponteiro foi clicado
            DateTime agora = DateTime.Now;

            // Ângulos dos ponteiros
            double anguloHora = (agora.Hour % 12 + agora.Minute / 60.0) * Math.PI / 6;
            double anguloMinuto = (agora.Minute + agora.Second / 60.0) * Math.PI / 30;
            double anguloSegundo = agora.Second * Math.PI / 30;

            // Pontas dos ponteiros
            Point pontoHora = new Point(
                cx + (int)(raio * 0.5 * Math.Sin(anguloHora)),
                cy - (int)(raio * 0.5 * Math.Cos(anguloHora)));

            Point pontoMinuto = new Point(
                cx + (int)(raio * 0.7 * Math.Sin(anguloMinuto)),
                cy - (int)(raio * 0.7 * Math.Cos(anguloMinuto)));

            Point pontoSegundo = new Point(
                cx + (int)(raio * 0.9 * Math.Sin(anguloSegundo)),
                cy - (int)(raio * 0.9 * Math.Cos(anguloSegundo)));

            // Verifica proximidade do clique
            if (Distancia(e.Location, pontoHora) < 10)
            {
                AlterarCor(ref corHora);
            }
            else if (Distancia(e.Location, pontoMinuto) < 10)
            {
                AlterarCor(ref corMinuto);
            }
            else if (Distancia(e.Location, pontoSegundo) < 10)
            {
                AlterarCor(ref corSegundo);
            }
        }

        private void AlterarCor(ref Color cor)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    cor = colorDialog.Color;
                    this.Invalidate();
                }
            }
        }

        private double Distancia(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Define o fundo
            g.Clear(corFundo);

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
            DesenhaPonteiro(g, cx, cy, anguloHora, raio * 0.5, corHora, 6);

            // Ponteiro dos minutos
            DesenhaPonteiro(g, cx, cy, anguloMinuto, raio * 0.7, corMinuto, 4);

            // Ponteiro dos segundos
            DesenhaPonteiro(g, cx, cy, anguloSegundo, raio * 0.9, corSegundo, 2);
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
