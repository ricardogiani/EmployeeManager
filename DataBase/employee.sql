CREATE TABLE IF NOT EXISTS employees (
    id UUID PRIMARY KEY,
    FirstName VARCHAR(100) NOT NULL,    -- AGORA PascalCase
    LastName VARCHAR(100) NOT NULL,     -- AGORA PascalCase
    Email VARCHAR(255) UNIQUE NOT NULL,
    DocumentNumber VARCHAR(20) UNIQUE NOT NULL,
    PhoneNumber VARCHAR(20),
    BirthDate DATE NOT NULL,
    JobLevel INTEGER NOT NULL,
    ManagerId UUID,
    PasswordHash VARCHAR(255) NOT NULL,
    PasswordSalt VARCHAR(255) NOT NULL,
    CreatedAt TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    UpdatedAt TIMESTAMP WITH TIME ZONE
);

-- (Índices e Foreign Keys permaneceriam os mesmos)
---

-- Add an index for faster lookups on email (useful for login/user retrieval)
CREATE INDEX IF NOT EXISTS idx_employees_email ON employees (email);

-- Add an index for document_number
CREATE INDEX IF NOT EXISTS idx_employees_document_number ON employees (DocumentNumber);

-- Add a foreign key constraint for manager_id (optional, but good for referential integrity)
/*ALTER TABLE employees
ADD CONSTRAINT fk_manager
FOREIGN KEY (manager_id) REFERENCES employees (id)
ON DELETE SET NULL;*/