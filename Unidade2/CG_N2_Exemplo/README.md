# CG_N2

Lembre que para usar os projetos no VSCode sempre abra o ```code-workspace``` (neste caso [CG_N2_Exemplo.code-workspace](CG_N2_Exemplo.code-workspace)) e manter o projeto ```CG_Biblioteca``` na pasta raiz deste repositório.

## Especificação  

A especificação deste projeto utilizou o [PlantUML](https://github.com/LDTTFURB/site/tree/main/ProjetosEnsino/Topicos/PlantUML) e [GraphViz](https://graphviz.org/).  

![include](https://www.plantuml.com/plantuml/svg/fLN1Zjem4BtdAonEGLrkFKKHMbYwgvNQBWMg7YkJc32LuqbjXz9k--zrdDWXWRJQXGEnnoypp_Dc9hTAKwdBd0S9fqgH9NB8K21v3eZvlV66iA2mOhleQVkDQRmX1GgDofr6xq7fU5dPtYwcu-RTECO3an9IDfsI3Qwr1965On8HPPV1dw1sH-BVauaF5JHSrd99EUYMYpDqkRXeCrsQqrzKsWW7XBH_i6HXmHv54gLORZaRWgqeO2ZDt_-XCv6L74Dg7uFm5-R1NTFbuKCfqZO9TXTcD6TVmAn-h9dMneUA7ivCDqHG3OS-yEC5uzXmQ1tKF6maBqR1GR1M2hlzfDEbaGFCnK7etgrho6krSczhczQciapPIJn2Zj4MaL-PPHl0NM_5T2sYlw6td5L-HMdtJdq_OQLnNHOBaCxqoWxmCHsjvj1pGbrHZ4Yofz8upvWsVkDOLK_Ni19oC0BJ3Ssfs5Ve7KUg1-3pBHujeVaSZusjZjg_DzhdFV0zzjIiTuEQBfXYOdzkjwvcAII0mfpkJZrtaG-IxZ1XWdPIU0AmQLUjBoE8b-8IyZ4D8NKrsm2xhhXcikkecdaA4eeNQlr9V1OcoMdBomixcKp9g1K52XnvZTZcPy4wGBMdAPDtA4TaRPU17yn2dqvbeIyLPh_-1ZLvyMWMxJow0vcSTagpxzFvMetGPY1QSgOEE2Pco8X_4Hq2NiM_eUY76SkgLckdMRs_sEdGQVDxoA_iyIQMrrLY9cbUTQKsSpanFfxD6y4j4sdrMJvR_0K0 "include")

## Arquivos

O exemplo de código ```CG_N2_Exemplo``` tem:

- .gitattributes e .gitignore: arquivos de controle do GitHub. São muito importantes para o uso correto do versionamento do código;  
- .vscode: pasta com arquivos de configuração do VSCode;  
  - launch.json: arquivo com configurações para depurar e executar o projeto;  
  - tasks.json: scripts para executar tarefas de compilação, depuração, execução e deploy do projeto;  
    - as tarefas podem ser executas usando: View / Command Palette / >Tasks: Run Task  
    - a tarefa para compilar pode ser executada usando: View / Command Palette / >Tasks: Run Build Task  
- CG_N2_Exemplo.code-workspace: arquivo do workspace que inclui o projeto ```CG_N2_Exemplo``` e ```CG_Biblioteca```;  
- CG_N2_Exemplo.csproj: arquivo do projeto;  
- bin: pasta com arquivos binários gerados para execução;  
- obj: pasta com arquivos binários gerados pela compilação;  
- README.md: este arquivo com descrições no formato MarkDown;  
- Program.cs: arquivo C# que instância os recursos (janela principal etc.) do OpenTK;  
- Shaders: pasta com os arquivos dos shaders (vertex and fragment);  
- Mundo.cs: arquivo C# que instância o Mundo e sobre-escreve métodos importantes do OpenTK;  
- Objeto.cs: arquivo C# que instância os Objetos Gráficos;  
- Ponto.cs: arquivo C# que instância o objeto gráfico Ponto;  
- SegReta.cs: arquivo C# que instância o objeto gráfico segmento de reta;  
- Retangulo.cs: arquivo C# que instância o objeto gráfico retângulo;  
- Poligono.cs: arquivo C# que instância o objeto gráfico polígono;  
- Circulo.cs: arquivo C# que deve ser implementado;  
- Spline.cs: arquivo C# que deve ser implementado;  
- SrPalito.cs: arquivo C# que deve ser implementado.  

As vezes também pode aparecer o arquivo ```.DS_Store``` que pode ser removido pois é é um arquivo que armazena atributos personalizados da pasta que a contém no sistema operacional Apple macOS.  

## Define

Outra aspecto que pode ser encontrado nas linhas destes códigos de exemplo são as declarações ```define```. Neste projeto, por exemplo, no arquivo [Mundo.cs](Mundo.cs) tem:  

```CSharp
#define CG_Gizmo  // debugar gráfico.
#define CG_OpenGL // render OpenGL.
//#define CG_DirectX // render DirectX.
//#define CG_Privado // código do professor.
````

Estas diretivas, as [Diretivas de pré-processador do C#](https://learn.microsoft.com/pt-br/dotnet/csharp/language-reference/preprocessor-directives), podem isolar alguns trechos de códigos se estiverem desabilitadas (só comentando a linha da diretiva). No caso, as linhas de código abaixo só serão compiladas/executadas/deploy se a diretiva ```#define CG_Gizmo``` estiver ativa pois permitem a [Compilação condicional](https://learn.microsoft.com/pt-br/dotnet/csharp/language-reference/preprocessor-directives#conditional-compilation).  

```CSharp
#if CG_Gizmo      
      Sru3D();
#endif
````

**Atenção**, a diretiva ```#define CG_Privado``` sempre de estar desabilitada pois a sua ativação faz o projeto utilizar arquivos C# disponíveis somente para o professor que devem ser implementados pelo aluno nas atividades desta unidade.  
