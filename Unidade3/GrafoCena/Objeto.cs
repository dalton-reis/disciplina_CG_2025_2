namespace GrafoCena
{
  internal class Objeto
  {
    private List<Objeto> objetoLista = [];
    private readonly char rotulo;
    // public char Rotulo { get => rotulo; set => rotulo = value; }

    public Objeto(ref char _rotulo)
    {
      rotulo = _rotulo = CharProximo(_rotulo);
      Console.WriteLine("Objeto: " + rotulo);
    }

    public void ObjetoFilhoAdicionar(ref char _rotulo)
    {
      Objeto objFilho = new Objeto(ref _rotulo);
      objetoLista.Add(objFilho);
    }

    private static char CharProximo(char atual) {
      return Convert.ToChar(atual + 1);
    }

  }
}