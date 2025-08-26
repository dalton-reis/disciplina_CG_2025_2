using System;

namespace CG_Biblioteca
{
  public static class Utilitario
  {
    public static char CharProximo(char atual)
    {
      return Convert.ToChar(atual + 1);
    }

    public static char TeclaUpperConsole(string msg, ref bool Control, ref bool Shift)
    {
      Console.WriteLine(msg);
      ConsoleKeyInfo estadoTeclado = Console.ReadKey(true);

      if ((estadoTeclado.Modifiers & ConsoleModifiers.Control) == ConsoleModifiers.Control)
      {
        Control = true;
      }
      else
      {
        if ((estadoTeclado.Modifiers & ConsoleModifiers.Shift) == ConsoleModifiers.Shift)
        {
          Shift = true;
        }
      }
      return char.ToUpper(estadoTeclado.Key.ToString()[0]);
    }

    // Coordenada de Dispositivo Normalizado (Normalized Device Coordinate - NDC)
    // Sistema de coordenadas de exibição independente das coordenadas de tela
    // Converte coordenada do ponto do espaço de tela para o espaço gráfico
    public static Ponto4D NDC_TelaSRU(int largura, int altura, Ponto4D mousePosition)
    {
      var x = 2 * (mousePosition.X / largura) - 1;
      var y = 2 * (-mousePosition.Y / altura) + 1;
      return new Ponto4D(x, y);
    }

    public static void AjudaTeclado()
    {
      Console.WriteLine(" --- Ajuda / Teclas: ");
      Console.WriteLine(" [H] mostra está ajuda. ");
      Console.WriteLine(" [Escape] sair. ");
      Console.WriteLine(" [Barra de Espaço] imprimir Grafo de Cena. "); //TODO: quais teclas irei usar.
    }

    public static void Diretivas()
    {
      Console.WriteLine("_ Diretivas de Compilação: _______ \n");
#if CG_OpenGL
      Console.WriteLine("CG_OpenGL - renderizado OpenGL");
#endif
#if CG_Gizmo
      Console.WriteLine("CG_Gizmo - objetos gráficos para depurar");
#endif
#if CG_DEBUG
      Console.WriteLine("CG_DEBUG - versão do código para depurar");
#endif      
#if CG_BBox
      Console.WriteLine("CG_BBox - exibe as BBox do objeto selecionado");
#endif
#if CG_Privado
      Console.WriteLine("CG_Privado - código do professor");
#endif
      Console.WriteLine("__________________________________ \n");
    }
  }
}