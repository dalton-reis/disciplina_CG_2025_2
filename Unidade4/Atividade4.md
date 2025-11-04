# Unidade 4: Conceitos Básicos 3D - atividade  

## Desafio (opcional)

Como desafio, proponho um trabalho opcional que, se realizado, acrescentará um ponto à sua média.  
A atividade consiste em responder a um questionário de avaliação de um TCC, que irá guiá-lo por etapas que envolvem implementação de código.  
Atenção: o ponto extra só será concedido se todas as instruções do questionário forem seguidas, e avançar no questionário para a parte do *desafio*:  
![image_TCC_questionario](image_TCC_questionario.png)  

**ATENÇÃO**: enviar e-mail para dalton@furb.br avisando que fez a atividade.  

Prazo: 20/06/2025.  

<https://docs.google.com/forms/d/e/1FAIpQLSfp7hZ6R7X6F13iZ59K1GvXUPwQq6xV5yRFYZ4sZp_rEqTX3w/viewform>  

## Atividade Individual

\[peso: 2,0] Esta atividade é para ser desenvolvida individualmente pelo aluno, no caso, não é em equipe. Essa atividade serve para reforçar/praticar os conceitos relacionados a Unidade 4.  

[Atividade4_VisEdu](Atividade4_VisEdu.md)  

## Atividade em Equipe

\[peso: 8,0]  

Lembretes:

- todas as questões deste trabalho devem ser desenvolvidas para serem executadas em um **único** projeto.  
- obrigatoriamente devem usar as classes disponibilizadas na Unidade2 e Unidade3, junto com as que foram criadas nesta unidade em [CG_N4_Exemplo](./CG_N4_Exemplo/).  
Mas **ATENÇÃO**, o [CG_N4_Exemplo](./CG_N4_Exemplo/) tem alguns ajustes para ser feito, como por exemplo, algumas faces do cubo não estão na orientação certa e devem ser ajustadas.  
- usem a pasta **Unidade4** do GitHub da sua equipe para desenvolver/entregar o seu código.  

## O que deve ser feito

Agora os principais objetivos do trabalho final (da Unidade 4) é usar os conceitos apresentados em aula sobre Câmera Sintética e considerando o uso correto do ```GL.Enable``` para usar funções de:

- Textura: ver exemplos [5-Textures](https://github.com/opentk/LearnOpenTK/tree/master/Chapter1/5-Textures) e [6-MultipleTextures](https://github.com/opentk/LearnOpenTK/tree/master/Chapter1/6-MultipleTextures);  
- Iluminação: [Chapter2](https://github.com/opentk/LearnOpenTK/tree/master/Chapter2).  

usando como fonte de pesquisa [LearnOpenTK](https://github.com/opentk/LearnOpenTK).  

Então desenvolva uma cena com dois cubos. Um cubo maior (fixo na origem do SRU) no centro da tela que tenha como textura em todas as suas faces uma foto dos integrantes da equipe. O cubo menor deve "orbitar" envolta do cubo maior girando em torno do eixo Z no sentido anti-horário. Com o uso do mouse se poderá visualizar envolta do cubo maior sempre olhando para o centro dele. E, por fim, poder mudar a iluminação usando as telas:

- tecla \[1]: 2-BasicLighting  
- tecla \[2]: 4-LightingMaps  
- tecla \[3]: 5-LightCasters-DirectionalLights  
- tecla \[4]: 5-LightCasters-PointLights  
- tecla \[5]: 5-LightCasters-Spotlight  
- tecla \[6]: 6-MultipleLights  
- tecla \[0]: sem iluminação

### Resumo dos Tipos de Luz — LearnOpenTK

| Exemplo                          | Tipo de Luz                              | Atenuação | Texturas de Iluminação | Direcionalidade         |
|----------------------------------|------------------------------------------|-----------|-------------------------|--------------------------|
| 2-BasicLighting                  | Direcional simples                       | Não       | Não                     | Sim                      |
| 4-LightingMaps                   | Direcional com texturas                  | Não       | Sim                     | Sim                      |
| 5-LightCasters-DirectionalLights | Direcional avançada                      | Não       | Não                     | Sim                      |
| 5-LightCasters-PointLights       | Ponto (pontual)                          | Sim       | Não                     | Não (omnidirecional)     |
| 5-LightCasters-Spotlight         | Spotlight (cone)                         | Sim       | Não                     | Sim (cone)               |
| 6-MultipleLights                 | Múltiplas (direcional, ponto, spotlight) | Sim       | Sim                     | Vários tipos             |
| 0-Sem iluminação                 | Nenhuma                                  | –         | Não                     | Não se aplica            |

🔹 2-BasicLighting
• Tipo de luz: Direcional simples  
• Características:  
  • Simula uma fonte de luz distante, como o Sol  
  • A direção da luz é constante (não importa a posição do objeto)  
  • Não há atenuação (a luz não perde intensidade com a distância)  
  • Usa o modelo de iluminação de Phong (ambiente + difusa + especular)  

🔹 4-LightingMaps
• Tipo de luz: Direcional, mas com texturas  
• Características:  
  • Introduz texturas de difusa e especular  
  • Permite que diferentes partes de um objeto reflitam a luz de forma distinta  
  • A luz ainda é direcional  
  • Torna a iluminação mais realista usando mapas (texturas) para refletância  

🔹 5-LightCasters-DirectionalLights
 • Tipo de luz: Direcional (mais complexa)  
 • Características:  
   • Similar ao exemplo “2-BasicLighting”, mas com mais refinamento e estrutura  
   • Usa structs e uniform blocks para organizar propriedades da luz  
   • É a base para expandir para outros tipos de luzes  

🔹 5-LightCasters-PointLights
• Tipo de luz: Ponto (pontual)  
• Características:  
  • Emite luz em todas as direções a partir de um ponto no espaço, como uma lâmpada  
  • Inclui atenuação com base na distância (mais realista)  
  • A intensidade da luz decresce com a distância  

🔹 5-LightCasters-Spotlight
• Tipo de luz: Spot (foco direcional)  
• Características:  
  • Emite luz em um cone, como uma lanterna  
  • Tem direção e ângulo de corte (cutoff)  
  • Usa dois ângulos para criar uma transição suave (soft edges)  
  • cutOff (ângulo interno)  
  • outerCutOff (ângulo externo)  

🔹 6-MultipleLights
• Tipo de luz: Múltiplas (direcional + pontos + spot)  
• Características:  
  • Combina vários tipos de luzes ao mesmo tempo  
  • 1 luz direciona  
  • 4 luzes pontuai  
  • 1 holofote (spotlight)  
  • Demonstra como gerenciar diferentes luzes simultaneamente no shader  
  • Exige organização avançada de dados e loops nos shaders  

🔹 0-sem iluminação
• Características:  
  • Nenhuma fonte de luz está ativa  
  • Os objetos aparecem com cor constante ou textura sem interação com luz  
  • Útil para comparar efeitos de iluminação versus ausência de iluminação  

## Gabarito

![Gabarito](atividadeGabarito.png "Gabarito")  

----------

## ⏭ [Início)](../README.md "Início")  
