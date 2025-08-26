# Ambiente de Desenvolvimento

Para o desenvolvimento do nosso código gráfico iremos usar a biblioteca OpenGL por intermédio do OpenTK, com a linguagem C# (SDK .Net) e a IDE VSCode.  

## SDK .Net

O primeiro passo é instalar o SDK do .NET Core para poder programar na linguagem C#.  

<https://www.microsoft.com/net/download>  

### Testar o SDK .NET

Para os passos a seguir é possível utilizar o prompt do Windows (cmd), terminal do MacOS (terminal), ou o próprio terminal do [VSCode](#ide-vscode).  
Crie uma nova pasta que será o diretório do projeto OpenTK no VSCode e navegue até ela. Nesse exemplo o nome da pasta será 'OlaMundo':  

  $ mkdir OlaMundo  
  $ cd OlaMundo  

Em seguida crie um ```Console Application``` nessa pasta:  

  $ dotnet new console  

Nesse ponto um novo arquivo Program.cs contendo um método main é criado. Para executar o projeto digite:  

  $ dotnet run  

Se o projeto foi criado corretamente, após a sua execução deve aparecer a mensagem 'Hello, World!' no terminal.  
Observe que no projeto foi criado dois arquivos:  

- OlaMundo.csproj  
- Program.cs  

E as pastas:

- bin  
- obj  

## IDE VSCode

O segundo passo é instalar a IDE VSCode (ou outra da sua escolha)  

<https://code.visualstudio.com/>  

Não esqueça de também instalar a extensão para CSharp <https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.vscode-dotnet-runtime>. E, esta extensão:

- C# Dev Kit: <https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit>  
- IntelliCode for C# Dev Kit: <https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.vscodeintellicode-csharp>  

Se necessário, tem mais informações sobre extensões do VSCode em: [https://code.visualstudio.com/docs/editor/extension-gallery](https://code.visualstudio.com/docs/editor/extension-gallery "https://code.visualstudio.com/docs/editor/extension-gallery")  

As extensões que eu uso podem ser vistas em: <https://github.com/dalton-reis/dalton-reis/blob/main/_._/VSCode/VsCodeExtensoes.md>  

### Testar o VSCode

Para testar a IDE VSCode abra o projeto criado.  

## Toolkit OpenTK

O terceiro passo é fazer com que o projeto use as dependências do Toolkit OpenTK (para usar o OpenGL no C#) no projeto criado:  

  $ dotnet add package OpenTK --version 4.8.2

### Testar o OpenTK

Nesse ponto, para testar se o OpenTK está funcionando acrescente a linha 'using OpenTK;' no cabeçalho da classe do arquivo Program.cs:  

  using OpenTK;  

Se nenhum erro ocorrer é porque o OpenTK já está disponível para ser usado.  
Caso ocorra algum erro de 'undefined command' tente executar o comando no terminal para recarregar as dependências do projeto:  

  $ dotnet restore

## [Voltar](./README.md#opentk)  
