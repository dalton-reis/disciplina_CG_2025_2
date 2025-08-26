using System;
using System.Collections.Generic;

namespace CG_Biblioteca
{
  public static class Grafocena
  {
    public static void GrafocenaAtualizar(Objeto mundo, Dictionary<char, Objeto> grafoLista)
    {
      grafoLista.Clear();
      grafoLista = mundo.GrafocenaAtualizar(grafoLista);
    }

    public static Objeto GrafoCenaProximo(Objeto mundo, Objeto objetoSelecionado, Dictionary<char, Objeto> grafoLista)
    {
      GrafocenaAtualizar(mundo, grafoLista);
      var itGrafo = grafoLista.GetEnumerator();
      itGrafo.MoveNext();
      itGrafo.MoveNext();
      if (objetoSelecionado == null)
      {
        objetoSelecionado = itGrafo.Current.Value;
        return objetoSelecionado;
      }
      if (objetoSelecionado.Rotulo == '@')
      {
        objetoSelecionado = itGrafo.Current.Value;
        return objetoSelecionado;
      }
      do
      {
        if (itGrafo.Current.Key == objetoSelecionado.Rotulo)
        {
          itGrafo.MoveNext();
          objetoSelecionado = itGrafo.Current.Value;
          return objetoSelecionado;
        }
      } while (itGrafo.MoveNext());
      return null;
    }

    public static void GrafoCenaImprimir(Objeto mundo, Dictionary<char, Objeto> grafoLista)
    {
      GrafocenaAtualizar(mundo, grafoLista);
      Console.WriteLine("__________________________________ \n");
      foreach (var par in grafoLista)
      {
        // Console.WriteLine($"Chave: {par.Key}, Valor: {par.Value}");
        Console.WriteLine($"Chave: {par.Key}");
      }
    }


  }
}