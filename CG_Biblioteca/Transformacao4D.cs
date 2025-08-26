using System;
using OpenTK.Mathematics;

namespace CG_Biblioteca
{
  /// <summary>
  /// Classe que define as Transformacoes Geometricas no espaco 3D
  /// Uma matriz de Transformacao eh representada por uma matriz 4x4 que acumula trasnformacoes, isto eh, 
  /// para aplicar as trasnformacoes T1, T2, em seguida, T3, eh necessario multiplicar T1 * T2 * T3. 
  /// Os valores de Translacao estao na coluna mais a direita.
  ///   Organizacao dos elementos internos da Matriz
  ///               [ matrix[ 0] matrix[ 4] matrix[ 8] matrix[12] ]
  ///   Transform = [ matrix[ 1] matrix[ 5] matrix[ 9] matrix[13] ]
  ///               [ matrix[ 2] matrix[ 6] matrix[10] matrix[14] ]
  ///               [ matrix[ 3] matrix[ 7] matrix[11] matrix[15] ]
  /// </summary>
  public class Transformacao4D
  {
    static public readonly double DEG_TO_RAD = 0.017453292519943295769236907684886;

    /// \brief Cria uma matriz de Trasnformacao com uma matriz Identidade.
	  private readonly double[] matriz = {
      1, 0, 0, 0,
      0, 1, 0, 0,
      0, 0, 1, 0,
      0, 0, 0, 1};

    public Transformacao4D()
    {
    }

#if CG_OpenGL
    ///               [ matrix[ 0](11) matrix[ 4](21) matrix[ 8](31) matrix[12](41) ]
    ///   Transform = [ matrix[ 1](12) matrix[ 5](22) matrix[ 9](32) matrix[13](42) ]
    ///               [ matrix[ 2](13) matrix[ 6](23) matrix[10](33) matrix[14](43) ]
    ///               [ matrix[ 3](14) matrix[ 7](24) matrix[11](34) matrix[15](44) ]

    // transform = transform * Matrix4.CreateScale(1.1f);
    // transform = transform * Matrix4.CreateTranslation(0.5f, 0.5f, 0.0f);
    // transform = transform * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(45f));
    public Matrix4 ObterDadosOpenTK()
    {
      var transform = Matrix4.Identity;
      transform.M11 = (float)matriz[0]; transform.M21 = (float)matriz[4]; transform.M31 = (float)matriz[8]; transform.M41 = (float)matriz[12];
      transform.M12 = (float)matriz[1]; transform.M22 = (float)matriz[5]; transform.M32 = (float)matriz[9]; transform.M42 = (float)matriz[13];
      transform.M13 = (float)matriz[2]; transform.M23 = (float)matriz[6]; transform.M33 = (float)matriz[10]; transform.M43 = (float)matriz[14];
      transform.M14 = (float)matriz[3]; transform.M24 = (float)matriz[7]; transform.M34 = (float)matriz[11]; transform.M44 = (float)matriz[15];

      return transform;
    }
#endif

    public void Inversa()
    {
      var transform = Matrix4.Identity;
      transform.M11 = (float)matriz[0]; transform.M12 = (float)matriz[4]; transform.M13 = (float)matriz[8]; transform.M14 = (float)matriz[12];
      transform.M21 = (float)matriz[1]; transform.M22 = (float)matriz[5]; transform.M23 = (float)matriz[9]; transform.M24 = (float)matriz[13];
      transform.M31 = (float)matriz[2]; transform.M32 = (float)matriz[6]; transform.M33 = (float)matriz[10]; transform.M34 = (float)matriz[14];
      transform.M41 = (float)matriz[3]; transform.M42 = (float)matriz[7]; transform.M43 = (float)matriz[11]; transform.M44 = (float)matriz[15];

      //FIXME: implementar em C# matriz inversa para não precisar ficar convertendo entre Transform4D e Matrix4
      transform = Matrix4.Invert(transform);

      matriz[0] = (float)transform.M11; matriz[4] = (float)transform.M12; matriz[8] = (float)transform.M13; matriz[12] = (float)transform.M14;
      matriz[1] = (float)transform.M21; matriz[5] = (float)transform.M22; matriz[9] = (float)transform.M23; matriz[13] = (float)transform.M24;
      matriz[2] = (float)transform.M31; matriz[6] = (float)transform.M32; matriz[10] = (float)transform.M33; matriz[14] = (float)transform.M34;
      matriz[3] = (float)transform.M41; matriz[7] = (float)transform.M42; matriz[11] = (float)transform.M43; matriz[15] = (float)transform.M44;
    }

    /// Atribui os valores de uma matriz Identidade a matriz de Transformacao.
    public void AtribuirIdentidade()
    {
      for (int i = 0; i < 16; ++i)
      {
        matriz[i] = 0.0;
      }
      matriz[0] = matriz[5] = matriz[10] = matriz[15] = 1.0;
    }

    /// Atribui os valores de Translacao (tx,ty,tz) a matriz de Transformacao.
    /// Elemento Neutro eh 0 (zero).
    public void AtribuirTranslacao(double tx, double ty, double tz)
    {
      AtribuirIdentidade();
      matriz[12] = tx;
      matriz[13] = ty;
      matriz[14] = tz;
    }

    /// Atribui o valor de Escala (Ex,Ey,Ez) a matriz de Transformacao.
    /// Elemento Neutro eh 1 (um).
    /// Se manter os valores iguais de Ex,Ey e Ez o objeto vai ser reduzido ou ampliado proporcionalmente.
    public void AtribuirEscala(double sX, double sY, double sZ)
    {
      AtribuirIdentidade();
      matriz[0] = sX;
      matriz[5] = sY;
      matriz[10] = sZ;
    }

    /// Atribui o valor de Rotacao (angulo) no eixo X a matriz de Transformacao.
    /// Elemento Neutro eh 0 (zero).
    public void AtribuirRotacaoX(double radians)
    {
      AtribuirIdentidade();
      matriz[5] = Math.Cos(radians);
      matriz[9] = -Math.Sin(radians);
      matriz[6] = Math.Sin(radians);
      matriz[10] = Math.Cos(radians);
    }

    /// Atribui o valor de Rotacao (angulo) no eixo Y a matriz de Transformacao.
    /// Elemento Neutro eh 0 (zero).
    public void AtribuirRotacaoY(double radians)
    {
      AtribuirIdentidade();
      matriz[0] = Math.Cos(radians);
      matriz[8] = Math.Sin(radians);
      matriz[2] = -Math.Sin(radians);
      matriz[10] = Math.Cos(radians);
    }

    /// Atribui o valor de Rotacao (angulo) no eixo Z a matriz de Transformacao.
    /// Elemento Neutro eh 0 (zero).
    public void AtribuirRotacaoZ(double radians)
    {
      AtribuirIdentidade();
      matriz[0] = Math.Cos(radians);
      matriz[4] = -Math.Sin(radians);
      matriz[1] = Math.Sin(radians);
      matriz[5] = Math.Cos(radians);
    }

    //TODO: tentar usar Const
    public Ponto4D MultiplicarPonto(Ponto4D pto)
    {
      Ponto4D pointResult = new(
          matriz[0] * pto.X + matriz[4] * pto.Y + matriz[8] * pto.Z + matriz[12] * pto.W,
          matriz[1] * pto.X + matriz[5] * pto.Y + matriz[9] * pto.Z + matriz[13] * pto.W,
          matriz[2] * pto.X + matriz[6] * pto.Y + matriz[10] * pto.Z + matriz[14] * pto.W,
          matriz[3] * pto.X + matriz[7] * pto.Y + matriz[11] * pto.Z + matriz[15] * pto.W);
      return pointResult;
    }

    public Transformacao4D MultiplicarMatriz(Transformacao4D t)
    {
      Transformacao4D result = new Transformacao4D();
      for (int i = 0; i < 16; ++i)
        result.matriz[i] =
              matriz[i % 4] * t.matriz[i / 4 * 4] + matriz[(i % 4) + 4] * t.matriz[i / 4 * 4 + 1]
            + matriz[(i % 4) + 8] * t.matriz[i / 4 * 4 + 2] + matriz[(i % 4) + 12] * t.matriz[i / 4 * 4 + 3];
      return result;
    }

    public double ObterElemento(int index)
    {
      return matriz[index];
    }

    public void AtribuirElemento(int index, double value)
    {
      matriz[index] = value;
    }

    public double[] ObterDados()
    {
      return matriz;
    }

    public void AtribuirDados(double[] data)
    {
      int i;

      for (i = 0; i < 16; i++)
      {
        matriz[i] = data[i];
      }
    }

#if CG_Debug
    public override string ToString()
    {
      Console.WriteLine("__________________________________ \n");
      string retorno;
      retorno = "__ Transformacao4D: " + "\n";
      retorno += "|" + ObterElemento(0) + " | " + ObterElemento(4) + " | " + ObterElemento(8) + " | " + ObterElemento(12) + "\n";
      retorno += "|" + ObterElemento(1) + " | " + ObterElemento(5) + " | " + ObterElemento(9) + " | " + ObterElemento(13) + "\n";
      retorno += "|" + ObterElemento(2) + " | " + ObterElemento(6) + " | " + ObterElemento(10) + " | " + ObterElemento(14) + "\n";
      retorno += "|" + ObterElemento(3) + " | " + ObterElemento(7) + " | " + ObterElemento(11) + " | " + ObterElemento(15) + "\n";
      return retorno;
    }
#endif

  }

}