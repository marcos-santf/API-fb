CREATE TABLE usuario (
    id_usuario INT IDENTITY(1,1) PRIMARY KEY,
    nome_usuario NVARCHAR(100) NOT NULL,
    email_usuario NVARCHAR(100) NOT NULL,
    senha_usuario NVARCHAR(100) NOT NULL,
    lembrete_senha NVARCHAR(100),
    tipo_usuario INT NOT NULL DEFAULT 1,
    fg_excluido BIT NOT NULL DEFAULT 0,
	ind_tentativas_login INT NOT NULL DEFAULT 0,
	fg_bloqueado BIT NOT NULL DEFAULT 0,
	fg_recupera_senha BIT NOT NULL DEFAULT 0,
	dt_inclusao DATETIME,
	dt_alteracao DATETIME
);

CREATE TABLE empresa (
    id_empresa INT IDENTITY(1,1) PRIMARY KEY,
    id_usuario INT NOT NULL,
    nome_empresa NVARCHAR(250) NOT NULL,
    app_id NVARCHAR(MAX),
    app_secret NVARCHAR(MAX),
    access_token NVARCHAR(MAX),
	dt_token DATETIME,
    business_id NVARCHAR(MAX),
    fg_excluido BIT NOT NULL DEFAULT 0,
	dt_inclusao DATETIME,
	dt_alteracao DATETIME,
    CONSTRAINT fk_empresa_id_usuario
    FOREIGN KEY (id_usuario)
    REFERENCES usuario (id_usuario)
);

CREATE TABLE empresa_campanhas (
    id_campanha INT IDENTITY(1,1) PRIMARY KEY,
    id_empresa INT NOT NULL,
    nr_campanha NVARCHAR(MAX),
    ds_campanha NVARCHAR(MAX),
    status_campanha NVARCHAR(100),
    date_start DATETIME,
    date_stop DATETIME,
    fg_excluido BIT NOT NULL DEFAULT 0,
	dt_inclusao DATETIME,
	dt_alteracao DATETIME,
    CONSTRAINT fk_empresa_campanhas_id_empresa
    FOREIGN KEY (id_empresa)
    REFERENCES empresa (id_empresa)
);

CREATE TABLE empresa_pixels (
    id_pixel INT IDENTITY(1,1) PRIMARY KEY,
    id_empresa INT NOT NULL,
    nr_campanha NVARCHAR(MAX),
    nr_pixel NVARCHAR(MAX),
    ds_pixel NVARCHAR(MAX),
    fg_excluido BIT NOT NULL DEFAULT 0,
	dt_inclusao DATETIME,
	dt_alteracao DATETIME,
    CONSTRAINT fk_empresa_pixels_id_empresa
    FOREIGN KEY (id_empresa)
    REFERENCES empresa (id_empresa)
);
