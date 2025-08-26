using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;

namespace CG_Biblioteca
{
  public class Objeto  //TODO: deveria ser uma class abstract ..??
  {
    // Objeto _______________________________________
    private readonly char rotulo;
    public char Rotulo { get => rotulo; }
    protected Objeto paiRef;
    private readonly List<Objeto> objetosLista = new List<Objeto>();
    private PrimitiveType primitivaTipo = PrimitiveType.LineLoop;
    public PrimitiveType PrimitivaTipo { get => primitivaTipo; set => primitivaTipo = value; }
    private float primitivaTamanho = 1;
    public float PrimitivaTamanho { get => primitivaTamanho; set => primitivaTamanho = value; }
    private Shader _shaderObjeto = new("Shaders/shader.vert", "Shaders/shaderBranca.frag");
    public Shader ShaderObjeto { set => _shaderObjeto = value; }

    // Vértices do objeto TODO: o objeto mundo deveria ter estes atributos abaixo?
    protected List<Ponto4D> pontosLista = [];
    public int PontosListaTamanho { get => pontosLista.Count; }
    private int _vertexBufferObject;
    private int _vertexArrayObject;

    // BBox do objeto _______________________________
    private readonly BBox bBox = new();
    public BBox Bbox()  //TODO: readonly
    {
      return bBox;
    }

    // Transformações do objeto _____________________
    private Transformacao4D matriz = new();
    private static Transformacao4D matrizGlobal = new();

    /// Matrizes temporarias que sempre sao inicializadas com matriz Identidade entao podem ser "static".
    private static Transformacao4D matrizTmpTranslacao = new();
    private static Transformacao4D matrizTmpTranslacaoInversa = new();
    private static Transformacao4D matrizTmpEscala = new();
    private static Transformacao4D matrizTmpRotacao = new();
    private static Transformacao4D matrizTmpGlobal = new();
    private char eixoRotacao = 'z';
    // public void TrocaEixoRotacao(char eixo) => eixoRotacao = eixo; [TODO: usar?]


    public Objeto(Objeto _paiRef, ref char _rotulo, Objeto objetoFilho = null)
    {
      this.paiRef = _paiRef;
      rotulo = _rotulo = Utilitario.CharProximo(_rotulo);
      if (_paiRef != null)
      {
        ObjetoAdicionar(objetoFilho);
      }
    }

    private void ObjetoAdicionar(Objeto objetoFilho)
    {
      if (objetoFilho == null)
      {
        paiRef.objetosLista.Add(this);
      }
      else
      {
        paiRef.FilhoAdicionar(objetoFilho);
      }
    }

    public void ObjetoRemover()
    {
      // remover objetos filhos
      while (objetosLista.Count > 0)
      {
        objetosLista[0].ObjetoRemover();
      }

      paiRef.objetosLista.Remove(this);

      // remover os filhos dele da lista de objetos
      objetosLista.Remove(this);

      // remover objeto
      OnUnload();
      objetosLista.Clear();
      pontosLista.Clear();
      // private BBox bBox = new BBox();  //TODO: preciso remover estes objetos?
      // private Transformacao4D matriz = new Transformacao4D();
      // private static Transformacao4D matrizTmpTranslacao = new Transformacao4D();
      // private static Transformacao4D matrizTmpTranslacaoInversa = new Transformacao4D();
      // private static Transformacao4D matrizTmpEscala = new Transformacao4D();
      // private static Transformacao4D matrizTmpRotacao = new Transformacao4D();
      // private static Transformacao4D matrizGlobal = new Transformacao4D();

      //this = null; não é permitido
    }

    public void ObjetoAtualizar()
    {
      //FIXME: deveria chamar OnUnload() mas sem ir para os filhos
      float[] vertices = new float[pontosLista.Count * 3];
      int ptoLista = 0;
      for (int i = 0; i < vertices.Length; i += 3)
      {
        vertices[i] = (float)pontosLista[ptoLista].X;
        vertices[i + 1] = (float)pontosLista[ptoLista].Y;
        vertices[i + 2] = (float)pontosLista[ptoLista].Z;
        ptoLista++;
      }

      GL.PointSize(primitivaTamanho);

      _vertexBufferObject = GL.GenBuffer();
      GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
      GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
      _vertexArrayObject = GL.GenVertexArray();
      GL.BindVertexArray(_vertexArrayObject);
      GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
      GL.EnableVertexAttribArray(0);

      matrizGlobal = ObjetoMatrizGlobal(matriz);    // Atualiza a matrizGlobal
      bBox.Atualizar(matrizGlobal, pontosLista);
    }

    public Transformacao4D ObjetoMatrizGlobal(Transformacao4D matrizGlobalPai)
    {
      if (rotulo != '@')
      {
        matrizGlobalPai = paiRef.matriz.MultiplicarMatriz(matrizGlobalPai);
        matrizGlobalPai = paiRef.ObjetoMatrizGlobal(matrizGlobalPai);
      }
      return matrizGlobalPai;
    }

    public void Desenhar(Transformacao4D matrizGrafo, Objeto objetoSelecionado)
    {
#if CG_OpenGL
      GL.PointSize(primitivaTamanho);

      GL.BindVertexArray(_vertexArrayObject);

      if (paiRef != null)
      {
        // ## 14. Grafo de cena: transformação
        // Considere a transformação global ao transformar (translação/escala/rotação) um polígono “pai”.  
        matrizGrafo = matrizGrafo.MultiplicarMatriz(matriz);
        _shaderObjeto.SetMatrix4("transform", matrizGrafo.ObterDadosOpenTK());
        _shaderObjeto.Use();
        GL.DrawArrays(primitivaTipo, 0, pontosLista.Count);
        if (objetoSelecionado == this)
        {
          matrizGlobal = ObjetoMatrizGlobal(matriz);
          // bBox.Atualizar(matrizGlobal, pontosLista);
#if CG_Gizmo
#if CG_BBox
          bBox.Desenhar();
#endif
#endif
        }
      }
      for (var i = 0; i < objetosLista.Count; i++)
      {
        objetosLista[i].Desenhar(matrizGrafo, objetoSelecionado);
      }
#endif
    }

    #region Objeto: CRUD

    public void FilhoAdicionar(Objeto filho)
    {
      this.objetosLista.Add(filho);
    }
    public Ponto4D PontosId(int id)
    {
      return pontosLista[id];
    }

    public void PontosAdicionar(Ponto4D pto)
    {
      pontosLista.Add(pto);
      ObjetoAtualizar();
    }

    public void PontosAlterar(Ponto4D pto, int posicao)
    {
      pontosLista[posicao] = pto;
      ObjetoAtualizar();
    }

    public void PontosApagar()
    {
      pontosLista.Clear();
      ObjetoAtualizar();
    }

    #endregion

    public Ponto4D MatrizGlobalInversa(Ponto4D mousePto)
    {
      matrizGlobal = ObjetoMatrizGlobal(matriz);    // Atualiza a matrizGlobal

      matrizGlobal.Inversa();
      return matrizGlobal.MultiplicarPonto(mousePto);
    }

    public int PontoMaisPerto(Ponto4D mousePto, bool remover = true)
    {
      var iMaisPerto = 0;
      double distMaisPerto = Matematica.DistanciaQuadrado(mousePto, pontosLista[iMaisPerto]); // assumindo polígono sempre tem mínimo dois pontos
      double distTMP;
      for (var i = 1; i < pontosLista.Count; i++) // 2a elemento
      {
        distTMP = Matematica.DistanciaQuadrado(mousePto, pontosLista[i]);
        if (distTMP < distMaisPerto)
        {
          distMaisPerto = distTMP;
          iMaisPerto = i;
        }
      }
      PontosAlterar(mousePto, iMaisPerto);

      if (remover)
      {
        pontosLista.RemoveAt(iMaisPerto);
        if (pontosLista.Count < 2)  // objeto no mínimo deve ter dois vértices
        {
          this.ObjetoRemover();
          return -1;
        }
        else
          ObjetoAtualizar();
      }
      return iMaisPerto;
    }

    #region Objeto: Grafo de Cena
    //TODO: estes métodos não deveriam estar na classe GrafoCena da CG_Biblioteca?

    public Objeto GrafocenaBusca(char _rotulo)
    {
      if (rotulo == _rotulo)
      {
        return this;
      }
      foreach (var objeto in objetosLista)
      {
        var obj = objeto.GrafocenaBusca(_rotulo);
        if (obj != null)
        {
          return obj;
        }
      }
      return null;
    }

    public Dictionary<char, Objeto> GrafocenaAtualizar(Dictionary<char, Objeto> lista)
    {
      lista.Add(rotulo, this);
      foreach (var objeto in objetosLista)
      {
        lista = objeto.GrafocenaAtualizar(lista);
      }
      return lista;
    }

    public void GrafocenaImprimir(string idt)
    {
      Console.WriteLine(idt + rotulo);
      foreach (var objeto in objetosLista)
      {
        objeto.GrafocenaImprimir(idt + "  ");
      }
    }

    #endregion

    #region Objeto: Transformações Geométricas

    public void MatrizImprimir()
    {
      System.Console.WriteLine(matriz);

      matrizGlobal = ObjetoMatrizGlobal(matriz);
      System.Console.WriteLine(matrizGlobal);
    }
    public void MatrizAtribuirIdentidade()
    {
      matriz.AtribuirIdentidade();
      ObjetoAtualizar();
    }
    public void MatrizTranslacaoXYZ(double tx, double ty, double tz)
    {
      Transformacao4D matrizTranslate = new Transformacao4D();
      matrizTranslate.AtribuirTranslacao(tx, ty, tz);
      matriz = matrizTranslate.MultiplicarMatriz(matriz);
      ObjetoAtualizar();
    }
    public void MatrizEscalaXYZ(double Sx, double Sy, double Sz)
    {
      Transformacao4D matrizScale = new Transformacao4D();
      matrizScale.AtribuirEscala(Sx, Sy, Sz);
      matriz = matrizScale.MultiplicarMatriz(matriz);
      ObjetoAtualizar();
    }

    public void MatrizEscalaXYZBBox(double Sx, double Sy, double Sz)
    {
      matrizTmpGlobal.AtribuirIdentidade();
      Ponto4D pontoPivo = bBox.ObterCentro;

      matrizTmpTranslacao.AtribuirTranslacao(-pontoPivo.X, -pontoPivo.Y, -pontoPivo.Z); // Inverter sinal
      matrizTmpGlobal = matrizTmpTranslacao.MultiplicarMatriz(matrizTmpGlobal);

      matrizTmpEscala.AtribuirEscala(Sx, Sy, Sz);
      matrizTmpGlobal = matrizTmpEscala.MultiplicarMatriz(matrizTmpGlobal);

      matrizTmpTranslacaoInversa.AtribuirTranslacao(pontoPivo.X, pontoPivo.Y, pontoPivo.Z);
      matrizTmpGlobal = matrizTmpTranslacaoInversa.MultiplicarMatriz(matrizTmpGlobal);

      matriz = matriz.MultiplicarMatriz(matrizTmpGlobal);

      ObjetoAtualizar();
    }
    public void MatrizRotacaoEixo(double angulo)
    {
      switch (eixoRotacao)  // TODO: ainda não uso no exemplo
      {
        case 'x':
          matrizTmpRotacao.AtribuirRotacaoX(Transformacao4D.DEG_TO_RAD * angulo);
          break;
        case 'y':
          matrizTmpRotacao.AtribuirRotacaoY(Transformacao4D.DEG_TO_RAD * angulo);
          break;
        case 'z':
          matrizTmpRotacao.AtribuirRotacaoZ(Transformacao4D.DEG_TO_RAD * angulo);
          break;
        default:
          Console.WriteLine("opção de eixoRotacao: ERRADA!");
          break;
      }
      ObjetoAtualizar();
    }
    public void MatrizRotacao(double angulo)
    {
      MatrizRotacaoEixo(angulo);
      matriz = matrizTmpRotacao.MultiplicarMatriz(matriz);
      ObjetoAtualizar();
    }
    public void MatrizRotacaoZBBox(double angulo)
    {
      matrizTmpGlobal.AtribuirIdentidade();
      Ponto4D pontoPivo = bBox.ObterCentro;

      matrizTmpTranslacao.AtribuirTranslacao(-pontoPivo.X, -pontoPivo.Y, -pontoPivo.Z); // Inverter sinal
      matrizTmpGlobal = matrizTmpTranslacao.MultiplicarMatriz(matrizTmpGlobal);

      MatrizRotacaoEixo(angulo);
      matrizTmpGlobal = matrizTmpRotacao.MultiplicarMatriz(matrizTmpGlobal);

      matrizTmpTranslacaoInversa.AtribuirTranslacao(pontoPivo.X, pontoPivo.Y, pontoPivo.Z);
      matrizTmpGlobal = matrizTmpTranslacaoInversa.MultiplicarMatriz(matrizTmpGlobal);

      matriz = matriz.MultiplicarMatriz(matrizTmpGlobal);

      ObjetoAtualizar();
    }

    #endregion

    public void OnUnload()
    {
      foreach (var objeto in objetosLista)
      {
        objeto.OnUnload();
      }

      GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
      GL.BindVertexArray(0);
      GL.UseProgram(0);

      GL.DeleteBuffer(_vertexBufferObject);
      GL.DeleteVertexArray(_vertexArrayObject);

      // GL.DeleteProgram(_shaderObjeto.Handle);
    }

    public bool ScanLine(Ponto4D ptoClique, ref Objeto objetoSelecionado)
    {
      if (rotulo != '@')
      {
        matrizGlobal = ObjetoMatrizGlobal(matriz);    // Atualiza a matrizGlobal
        bBox.Atualizar(matrizGlobal, pontosLista);

        // Teste de seleção do polígono usando a BBox  
        if (bBox.Dentro(ptoClique))
        {
          // Teste de seleção do polígono usando o algoritmo ScanLine  
          ushort paridade = 0;
          if (pontosLista.Count >= 2)
          {
            for (var i = 0; i < (pontosLista.Count - 1); i++)
            {
              if (Matematica.ScanLine(ptoClique, matrizGlobal.MultiplicarPonto(pontosLista[i]), matrizGlobal.MultiplicarPonto(pontosLista[i + 1])))
                paridade++;
            }
            if (Matematica.ScanLine(ptoClique, matrizGlobal.MultiplicarPonto(pontosLista[pontosLista.Count - 1]), matrizGlobal.MultiplicarPonto(pontosLista[0]))) // último/primeiro segmento
              paridade++;
          }
          if ((paridade % 2) != 0)  // dentro polígono
          {
            objetoSelecionado = this;     // dentro polígono
            return true;
          }
        }
      }

      // fora polígono
      foreach (var objeto in objetosLista)
      {
        if (objeto.ScanLine(ptoClique, ref objetoSelecionado))
          return true;
      }
      return false;
    }

#if CG_Debug
    protected string ImprimeToString()
    {
      Console.WriteLine("__________________________________ \n");
      string retorno;
      retorno = "__ Objeto Original: " + rotulo + "\n";
      for (var i = 0; i < pontosLista.Count; i++)
      {
        retorno += "P" + i + "[ " +
        string.Format("{0,10}", pontosLista[i].X) + " | " +
        string.Format("{0,10}", pontosLista[i].Y) + " | " +
        string.Format("{0,10}", pontosLista[i].Z) + " | " +
        string.Format("{0,10}", pontosLista[i].W) + " ]" + "\n";
      }

      retorno += "__ Objeto Transformado: " + rotulo + "\n";
      for (var i = 0; i < pontosLista.Count; i++)
      {
        retorno += "P" + i + "[ " +
        string.Format("{0,10}", pontosLista[i].X) + " | " +
        string.Format("{0,10}", pontosLista[i].Y) + " | " +
        string.Format("{0,10}", pontosLista[i].Z) + " | " +
        string.Format("{0,10}", pontosLista[i].W) + " ]" + "\n";
      }

      bBox.Atualizar(ObjetoMatrizGlobal(matriz), pontosLista);
      retorno += bBox.ToString();
      return retorno;
    }
#endif

  }
}