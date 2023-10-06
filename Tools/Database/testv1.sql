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
                          maladie integer,
                          images integer,
                          CONSTRAINT fk_PatientsImage
                              FOREIGN KEY (images)
                                  REFERENCES Image_Patient(id),
                          CONSTRAINT fk_PatientsMaladie
                              FOREIGN KEY (maladie)
                                  REFERENCES Maladies(id)
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
