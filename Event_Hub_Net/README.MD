# Event Hub Example C#

## Objetivo
O objetivo desse projeto é consumir o event Hub da Azure, através via .Net, essa aplicação funciona via console, e possui uma pequena iteração.

### Variaveis

Toda conexão da aplicação é feito via várivel de ambiente, voce pode encontrar as variaveis de ambiente no `Program.cs`

`//Variavel de conexão do blob Azure Blob Storage "blobStorageConnectionString"`</br>   
`"//Variavel com o nome do Container Blob blobContainerName"`</br>
`"//Variavel com a String de conexão de conexão do Evennt HubconnectionStringHub"`</br>
`"//Variavel com o nome do topico do EventHub eventHub"`</br>

### Modulo de Escrita

As mensagens escritas são fixas e estão na classe `WriteEventHub.cs`.</br>
Após escrever a menssagem já é emitido um log dizendo que as mensagens foram escritas.

### Módulo de Leitura

O módulo de leitura pode ser encontrado na classe `ReadEventHub.cs`.</br>
A diferença é de que o módulo de leitura utiliza não só o EventHub mas também utiliza o Azure Storage tables.

