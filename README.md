Relógio Analógico em Windows Forms
Este projeto implementa um relógio analógico simples usando a biblioteca Windows Forms em C#. O programa exibe um mostrador de relógio com ponteiros funcionais para horas, minutos e segundos, além de números indicativos para facilitar a leitura do horário.

Funcionalidades
Desenho dinâmico do mostrador do relógio.
Atualização automática a cada segundo para refletir o horário atual.
Ponteiros de horas, minutos e segundos desenhados proporcionalmente.
Números de 1 a 12 exibidos no mostrador.

Estrutura do Código
Classe Watcher
Descrição: Esta classe herda de Form e é responsável por toda a interface gráfica e funcionalidade do relógio.
Principais Métodos:
OnPaint: Sobrescreve o método de pintura para desenhar o mostrador, os números e os ponteiros do relógio.
DesenhaPonteiro: Função auxiliar para desenhar os ponteiros de acordo com o horário atual.

Funcionamento
Um Timer é usado para atualizar o relógio a cada segundo, chamando o método Invalidate para redesenhar o mostrador.
O método OnPaint calcula os ângulos de cada ponteiro com base no horário atual e os desenha no centro do formulário.
Números indicativos (de 1 a 12) são posicionados proporcionalmente ao redor do mostrador.
