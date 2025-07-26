CREATE TABLE IF NOT EXISTS employees (
    id BIGSERIAL PRIMARY KEY, -- ID único, chave primária, gerado automaticamente
    active BOOLEAN NOT NULL DEFAULT TRUE,          -- Status ativo, padrão TRUE
    first_name VARCHAR(100) NOT NULL,              -- Primeiro nome
    last_name VARCHAR(100) NOT NULL,               -- Sobrenome
    email VARCHAR(255) UNIQUE NOT NULL,            -- E-mail, único e não nulo
    document_number VARCHAR(20) UNIQUE NOT NULL,   -- Número do documento, único e não nulo
    phone_number VARCHAR(20),                      -- Número de telefone
    birth_date DATE NOT NULL,                      -- Data de nascimento
    job_level INTEGER NOT NULL,                    -- Nível do cargo (como INTEGER, correspondendo ao enum em C#)
    manager_id BIGINT,                               -- ID do gerente ()
    password_hash VARCHAR(255),           -- Hash da senha
    password_salt VARCHAR(255),           -- Salt da senha
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW(), -- Data de criação, preenchido automaticamente
    updated_at TIMESTAMP WITH TIME ZONE                -- Data da última atualização (anulável)
);

-- Add an index for faster lookups on email (useful for login/user retrieval)
CREATE INDEX IF NOT EXISTS idx_employees_email ON employees (email);

-- Add an index for document_number
CREATE INDEX IF NOT EXISTS idx_employees_document_number ON employees (document_number);


-- Add a foreign key constraint for manager_id (optional, but good for referential integrity)
/*ALTER TABLE employees
ADD CONSTRAINT fk_manager
FOREIGN KEY (manager_id) REFERENCES employees (id)
ON DELETE SET NULL;*/