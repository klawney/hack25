CREATE TABLE dbo.PRODUTO (
  CO_PRODUTO int NOT NULL primary key,
  NO_PRODUTO varchar(200) NOT NULL,
  PC_TAXA_JUROS numeric(10, 9) NOT NULL,
  NU_MINIMO_MESES smallint NOT NULL,
  NU_MAXIMO_MESES smallint NULL,
  VR_MINIMO numeric(18, 2) NOT NULL,
  VR_MAXIMO numeric(18, 2) NULL
);

INSERT INTO dbo.PRODUTO (CO_PRODUTO, NO_PRODUTO, PC_TAXA_JUROS,
  NU_MINIMO_MESES, NU_MAXIMO_MESES, VR_MINIMO, VR_MAXIMO)
VALUES (1, 'Produto 1', 0.017900000, 0, 24, 200.00, 10000.00)

INSERT INTO dbo.PRODUTO (CO_PRODUTO, NO_PRODUTO, PC_TAXA_JUROS,
  NU_MINIMO_MESES, NU_MAXIMO_MESES, VR_MINIMO, VR_MAXIMO)
VALUES (2, 'Produto 2', 0.017500000, 25, 48, 10001.00, 100000.00)

INSERT INTO dbo.PRODUTO (CO_PRODUTO, NO_PRODUTO, PC_TAXA_JUROS,
  NU_MINIMO_MESES, NU_MAXIMO_MESES, VR_MINIMO, VR_MAXIMO)
VALUES (3, 'Produto 3', 0.018200000, 49, 96, 100000.01, 1000000.00)

INSERT INTO dbo.PRODUTO (CO_PRODUTO, NO_PRODUTO, PC_TAXA_JUROS,
  NU_MINIMO_MESES, NU_MAXIMO_MESES, VR_MINIMO, VR_MAXIMO)
VALUES (4, 'Produto 4', 0.015100000, 96, null, 1000000.01, null)



1.  O que é API: [https://www.redhat.com/pt-br/topics/api/what-is-a-rest-api](https://www.redhat.com/pt-br/topics/api/what-is-a-rest-api)
2.  Calculadora SAC e Price: [https://calculojuridico.com.br/calculadora-price-sac/](https://calculojuridico.com.br/calculadora-price-sac/)
3.  O que é EventHub: [https://learn.microsoft.com/pt-br/azure/event-hubs/event-hubs-about](https://learn.microsoft.com/pt-br/azure/event-hubs/event-hubs-about)
4.  SQL Server: [https://learn.microsoft.com/pt-br/sql/sql-server?view=sql-server-ver-16](https://www.google.com/search?q=https://learn.microsoft.com/pt-br/sql/sql-server%3Fview%3Dsql-server-ver-16)
