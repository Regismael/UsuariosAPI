﻿CREATE TABLE USUARIO (
    ID  UNIQUEIDENTIFIER PRIMARY KEY,  
    NOME NVARCHAR(100) NOT NULL,       
    EMAIL NVARCHAR(100) NOT NULL,      
    SENHA NVARCHAR(255) NOT NULL,      
    SENHACONFIRMACAO NVARCHAR(255) NOT NULL 
);