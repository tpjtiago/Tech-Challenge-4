# Tech-Challenge-4

### Link Youtube: https://youtu.be/Yvq8xnuOIZw

Esse projeto tem como objetivo criar um <u>Sistema de Reservas para Coworking</u>, utilizando-se da arquitetura limpa e 
focando na separação de preocupações e independência entre UI, regras de negócio e infraestrutura, 
utilizando testes unitários e de integração.

### Devido ao alto relacionamento entre as entidades utilizadas, decidimos utilizar o banco SQL Server

# Funcionalidades

As funcionalidades do sistema incluem:

1. <b>Cadastrar novos clientes:</b><br>
    O sistema é capaz de cadastrar/alterar os dados de clientes que farão a reserva do coworking, com Nome, CPF e Dados de Contato.<br>
    Dentre esses dados, o Email é obrigatório e único entre clientes, e o CPF precisa conter 11 caracteres, somente numéricos
2. <b>Cadastrar espaços de Coworking</b><br>
    O sistema faz o cadastro/alteração de locais de coworking, com Nome, Descrição, Endereço e Horários de Abertura e Fechamento.<br>
    Os horários são obrigatórios e não podem estar fora da faixa de 24 horas de um dia, o horário de abertura também não pode ser maior que a hora de fechamento
3. <b>Cadastro de Salas:</b><br>
    É possível realizar o cadastro de salas dentro de um local de Coworking. Assim como alterar seus dados.<br>
    A sala deve conter Nome, uma Capacidade de Pessoas maior que 0, e seu Preço por Hora, que não pode ser menor que 0
4. <b>Realizar Reservas:</b><br>
    O sistema é capaz de fazer a reserva de um lugar na sala para o cliente, em uma faixa de horário e data específicas.<br>
    A reserva terá o status de PENDENTE enquanto não for realizado o pagamento.<br>
    O cliente não poderá realizar a reserva se:
    1. A faixa de horário solicitada iniciar antes da hora de abertura do local
    2. A faixa de horário solicitada terminar depois da hora de fechamento do local
    3. A sala já estiver com capacidade máxima durante a faixa de horário solicitada
    4. A data e horário solicitados forem anteriores a data atual
5. <b>Confirmar Pagamentos:</b><br>
    O sistema não faz o processamento do pagamento em si, mas recebe os dados do pagamento realizado por fora.<br>
    Se o valor informado for diferente do valor a ser pago para a reserva, a reserva não será confirmada e o status de pagamento ficará como FALHA. Não existe um limite para tentativas de pagamento<br>
    Se o valor informado for igual ao valor da reserva, será marcada a data de pagamento informada, o status de pagamento será marcado como CONCLUÍDO, e a reserva ficará como CONFIRMADA
6. <b>Cancelamento da Reserva:</b><br>
    Uma reserva CONFIRMADA pode ser cancelada, desde que a operação ocorra antes do horário inicial da mesma.<br>
    Uma reserva PENDENTE pode ser cancelada a qualquer momento.
7. <b>Marcação de Presença:</b><br>
    O sistema marca se o cliente compareceu no horário reservado.<br>
    A marcação não poderá ser feita se:
    1. A data atual for diferente da data reservada
    2. A hora atual não estiver entre 1 hora mais cedo do início da reserva e 1 hora mais tarde do final da reserva.<br> Por exemplo: se a reserva for das 10:00-11:00, a marcação poderá ser feita entre 9:00-12:00.
    3. O cliente já possuir presença em uma reserva na mesma faixa de horário e data, mas em um local diferente.

# Execução do Projeto
1. Instale o .NET 8.
2. Instale o banco de dados SQL Server.
3. Abra o projeto em uma IDE (Visual Studio Code ou Visual Studio 2022).
4. Configure o projeto Tech.Challenge4.API como o projeto de inicialização (Set as Start Project).
4. Abra o Package Manager Console e execute o comando Update-Database com o projeto Tech.Challenge4.Data selecionado.
5. Execute o projeto de API (dotnet run ou use o botão de start do Visual Studio).

# Tecnologias usadas no projeto
1. .NET 8
2. Entity Framework 8.0.6
3. AutoMapper
4. SQL Server
5. Dependency Injection
6. FluentValidation
7. Swagger
