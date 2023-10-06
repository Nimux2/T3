CREATE TABLE Questions (
                           id integer PRIMARY KEY AUTOINCREMENT,
                           texte text,
                           stress_effect integer,
                           diagno_effect integer
);

CREATE TABLE Traits (
                        id integer PRIMARY KEY AUTOINCREMENT,
                        nom text,
                        stress_effect real,
                        diagno_effect real
);

CREATE TABLE Patients (
                          id integer PRIMARY KEY AUTOINCREMENT,
                          nom text,
                          stress integer,
                          diagno integer,
                          images integer,
                          CONSTRAINT fk_PatientsImage
                              FOREIGN KEY (images)
                                  REFERENCES Image_Patient(id)
);

CREATE TABLE Reponses (
                          id integer PRIMARY KEY AUTOINCREMENT,
                          texte text
);

CREATE TABLE Maladies (
                          id integer PRIMARY KEY AUTOINCREMENT,
                          nom text
);

CREATE TABLE Symptomes (
                           id integer PRIMARY KEY AUTOINCREMENT,
                           nom text
);

CREATE TABLE Maladie_Symptome (
                                  id_maladie integer,
                                  id_symptome integer,
                                  CONSTRAINT fk_MSM
                                      FOREIGN KEY (id_maladie)
                                          REFERENCES Maladies(id),
                                  CONSTRAINT fk_MSS
                                      FOREIGN KEY (id_symptome)
                                          REFERENCES Symptomes(id)
);

CREATE TABLE Questions_Reponses (
                                    id_question integer,
                                    id_reponse integer,
                                    CONSTRAINT fk_QRQ
                                        FOREIGN KEY (id_question)
                                            REFERENCES Questions(id),
                                    CONSTRAINT fk_QRR
                                        FOREIGN KEY (id_reponse)
                                            REFERENCES Reponses(id)
);

CREATE TABLE Questions_Symptomes (
                                     id_question integer,
                                     id_symptome integer,
                                     CONSTRAINT fk_QSQ
                                         FOREIGN KEY (id_question)
                                             REFERENCES Questions(id),
                                     CONSTRAINT fk_QSS
                                         FOREIGN KEY (id_symptome)
                                             REFERENCES Symptomes(id)
);

CREATE TABLE Traits_Patients (
                                 id_trait integer,
                                 id_patient integer,
                                 CONSTRAINT fk_TPT
                                     FOREIGN KEY (id_trait)
                                         REFERENCES Traits(id),
                                 CONSTRAINT fk_TPP
                                     FOREIGN KEY (id_patient)
                                         REFERENCES Patients(id)
);

CREATE TABLE Reponses_Traits (
                                 id_reponse integer,
                                 id_trait integer,
                                 CONSTRAINT fk_PTP
                                     FOREIGN KEY (id_reponse)
                                         REFERENCES Reponses(id),
                                 CONSTRAINT fk_RTT
                                     FOREIGN KEY (id_trait)
                                         REFERENCES Traits(id)
);

CREATE TABLE Image_Patient (
                               id integer PRIMARY KEY AUTOINCREMENT,
                               img_default text NOT NULL,
                               img_triste text,
                               img_content text,
                               img_peur text,
                               img_colere text
);

INSERT INTO Maladies (id, nom) VALUES
  (1, 'Cancer'),
  (2, 'Diabète'),
  (3, 'Hypertension'),
  (4, 'Asthme'),
  (5, 'Arthrite'),
  (6, "Maladie d'Alzheimer"),
  (7, 'Maladie de Parkinson'),
  (8, 'Sclérose en plaques'),
  (9, 'VIH/SIDA'),
  (10, 'Maladie de Lyme'),
  (11, 'Anglais'),
  (12, 'Maladie cardiovasculaire'),
  (13, 'Obésité'),
  (14, 'Dépression'),
  (15, 'Anxiété');

INSERT INTO Patients (nom,stress,diagno) VALUES
	('Françis',0,0),
	('Bob',0,0),
	('Michel',0,0),
	('Saïf',0,0),
	('Yann',0,0),
	('Thomas',0,0),
	('Daniel',0,0),
	('Jean-Michel-François-d''Esperey',0,0),
	('Mamadou',0,0),
	('Mohammed',0,0),
	('Bernard',0,0),
	('Tituan',0,0),
	('Jean Lassale',0,0),
	('Manu',0,0),
	('Big Joe',0,0),
	('Zimmy',0,0),
	('Kim',0,0),
	('Eric',0,0),
	('Marine',0,0),
	('Jean-Mâ',0,0),
	('Angela',0,0),
	('Chirac',0,0),
	('Sarko',0,0),
	('Donald',0,0);

INSERT INTO Traits(nom,stress_effect,diagno_effect) VALUES
	('Stupide',25,25),
	('Raciste',50,40),
	('Paranoïaque',90,80),
	('Gentil',30,45),
	('Jovial',25,30),
	('Intelligent',60,90);
