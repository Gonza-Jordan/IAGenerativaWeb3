-- Tabla de Palabras Clave
CREATE TABLE PalabraClave (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Palabra NVARCHAR(100) NOT NULL
);

-- Tabla puente PalabraClave-Ambito (N a N)
CREATE TABLE PalabraClaveAmbito (
    Id INT PRIMARY KEY IDENTITY(1,1),
    PalabraClaveId INT NOT NULL,
    AmbitoId INT NOT NULL,
    FOREIGN KEY (PalabraClaveId) REFERENCES PalabraClave(Id),
    FOREIGN KEY (AmbitoId) REFERENCES Ambito(Id)
);

--Insert de palabras
INSERT INTO PalabraClave (Palabra) VALUES
('estimado'),
('cliente'),
('profesor'),
('tarea'),
('mamá'),
('amigo'),
('compra'),
('presupuesto'),
('cena'),
('fiesta'),
('universidad'),
('venta'),
('familia');

--Relacion con ambitos:
-- estimado: Laboral/Profesional, Educativo
INSERT INTO PalabraClaveAmbito (PalabraClaveId, AmbitoId) VALUES (1, 1), (1, 2);

-- cliente: Laboral/Profesional, Comercial
INSERT INTO PalabraClaveAmbito (PalabraClaveId, AmbitoId) VALUES (2, 1), (2, 5);

-- profesor: Educativo
INSERT INTO PalabraClaveAmbito (PalabraClaveId, AmbitoId) VALUES (3, 2);

-- tarea: Educativo
INSERT INTO PalabraClaveAmbito (PalabraClaveId, AmbitoId) VALUES (4, 2);

-- mamá: Familiar
INSERT INTO PalabraClaveAmbito (PalabraClaveId, AmbitoId) VALUES (5, 3);

-- amigo: Amistoso, Familiar
INSERT INTO PalabraClaveAmbito (PalabraClaveId, AmbitoId) VALUES (6, 4), (6, 3);

-- compra: Comercial
INSERT INTO PalabraClaveAmbito (PalabraClaveId, AmbitoId) VALUES (7, 5);

-- presupuesto: Comercial, Laboral/Profesional
INSERT INTO PalabraClaveAmbito (PalabraClaveId, AmbitoId) VALUES (8, 5), (8, 1);

-- cena: Familiar
INSERT INTO PalabraClaveAmbito (PalabraClaveId, AmbitoId) VALUES (9, 3);

-- fiesta: Amistoso
INSERT INTO PalabraClaveAmbito (PalabraClaveId, AmbitoId) VALUES (10, 4);

-- universidad: Educativo
INSERT INTO PalabraClaveAmbito (PalabraClaveId, AmbitoId) VALUES (11, 2);

-- venta: Comercial
INSERT INTO PalabraClaveAmbito (PalabraClaveId, AmbitoId) VALUES (12, 5);

-- familia: Familiar
INSERT INTO PalabraClaveAmbito (PalabraClaveId, AmbitoId) VALUES (13, 3);
