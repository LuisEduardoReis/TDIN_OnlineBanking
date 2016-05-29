Passos para executar as diferentes aplicações de Online Banking.
1 - Ligar o servidor que alberga o serviço central (Rest Server)
    1.1 - Compilar o projeto RestServer
    1.2 - Copiar o ficheiro de base de dados RestServer.db para a pasta onde foi criado o ficheiro executável do servidor.
    1.3 - Correr o executavel RestServer.exe como administrador
2 - Ligar a aplicação do Balcão
    2.1 - Compilar o projeto Counter
    2.2 - Correr o executavel Counter.exe gerado
3 - Ligar a aplicação do Serviço Bolsista
    3.1 - Compilar o projeto BankingDepartment
    3.2 - Copiar o ficheiro de base de dados BankingDepartment.db para a pasta onde foi criado o ficheiro executável do serviço.
    3.3 - Correr o executavel BankingDepartment.exe
4 - Ligar o cliente Web
    4.1 - Correr o executável mongoose-free-5.5.exe
    4.2 - Navegar até http:\\localhost:8080 num browser (caso não abra automaticamente)