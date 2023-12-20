BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "Traits" (
	"id"	integer,
	"nom"	varchar(255),
	"stress_effect"	real,
	"diagno_effect"	real,
	PRIMARY KEY("id")
);
CREATE TABLE IF NOT EXISTS "Maladies" (
	"id"	integer,
	"nom"	varchar(255),
	PRIMARY KEY("id")
);
CREATE TABLE IF NOT EXISTS "Symptomes" (
	"id"	integer,
	"nom"	varchar(255),
	PRIMARY KEY("id")
);
CREATE TABLE IF NOT EXISTS "Maladies_Symptomes" (
	"id_maladie"	integer,
	"id_symptome"	integer,
	CONSTRAINT "fk_MSM" FOREIGN KEY("id_maladie") REFERENCES "Maladies"("id"),
	CONSTRAINT "fk_MSS" FOREIGN KEY("id_symptome") REFERENCES "Symptomes"("id")
);
CREATE TABLE IF NOT EXISTS "Questions_Symptomes" (
	"id_question"	integer,
	"id_symptome"	integer,
	CONSTRAINT "fk_QSQ" FOREIGN KEY("id_question") REFERENCES "Questions"("id"),
	CONSTRAINT "fk_QSS" FOREIGN KEY("id_symptome") REFERENCES "Symptomes"("id")
);
CREATE TABLE IF NOT EXISTS "Traits_Patients" (
	"id_trait"	integer,
	"id_patient"	integer,
	CONSTRAINT "fk_TPT" FOREIGN KEY("id_trait") REFERENCES "Traits"("id"),
	CONSTRAINT "fk_TPP" FOREIGN KEY("id_patient") REFERENCES "Patients"("id")
);
CREATE TABLE IF NOT EXISTS "Reponses" (
	"id"	integer,
	"texte"	varchar(255),
	"stress"	integer NOT NULL DEFAULT 0,
	PRIMARY KEY("id")
);
CREATE TABLE IF NOT EXISTS "Reponses_Symptomes" (
	"id_symptome"	integer,
	"id_reponse"	integer,
	CONSTRAINT "fk_MSS" FOREIGN KEY("id_symptome") REFERENCES "Symptomes"("id"),
	CONSTRAINT "fk_QRR" FOREIGN KEY("id_reponse") REFERENCES "Reponses"("id")
);
CREATE TABLE IF NOT EXISTS "Questions" (
	"id"	integer,
	"ordre"	integer,
	"texte"	varchar(255),
	"stress_effet"	integer,
	"diagno_effet"	integer,
	PRIMARY KEY("id")
);
CREATE TABLE IF NOT EXISTS "ImagesPatient" (
	"id"	integer,
	"img_default"	varchar(255) NOT NULL,
	"img_triste"	varchar(255),
	"img_content"	varchar(255),
	"img_peur"	varchar(255),
	"img_colere"	varchar(255),
	PRIMARY KEY("id")
);
CREATE TABLE IF NOT EXISTS "Patients" (
	"id"	integer,
	"nom"	varchar(255) NOT NULL DEFAULT 'anonyme',
	"stress"	integer NOT NULL DEFAULT 0,
	"diagno"	integer NOT NULL DEFAULT 0,
	"images"	integer NOT NULL DEFAULT 1,
	CONSTRAINT "fk_PatientsImage" FOREIGN KEY("images") REFERENCES "ImagesPatient"("id"),
	PRIMARY KEY("id" AUTOINCREMENT)
);
INSERT INTO "Maladies" VALUES (1,'Cancer');
INSERT INTO "Maladies" VALUES (2,'Diabète');
INSERT INTO "Maladies" VALUES (3,'Hypertension');
INSERT INTO "Symptomes" VALUES (1,'sans symptome');
INSERT INTO "Symptomes" VALUES (2,'Essoufflement');
INSERT INTO "Symptomes" VALUES (3,'Sein qui gonfle');
INSERT INTO "Symptomes" VALUES (4,'Douleurs inexpliquées');
INSERT INTO "Symptomes" VALUES (5,'Tousser du sang');
INSERT INTO "Symptomes" VALUES (6,'soif intense');
INSERT INTO "Symptomes" VALUES (7,'Urine abondante');
INSERT INTO "Symptomes" VALUES (8,'Trouble de la vision');
INSERT INTO "Symptomes" VALUES (9,'Maux de tête');
INSERT INTO "Symptomes" VALUES (10,'Saignements de nez');
INSERT INTO "Maladies_Symptomes" VALUES (1,2);
INSERT INTO "Maladies_Symptomes" VALUES (1,3);
INSERT INTO "Maladies_Symptomes" VALUES (1,4);
INSERT INTO "Maladies_Symptomes" VALUES (1,5);
INSERT INTO "Maladies_Symptomes" VALUES (2,6);
INSERT INTO "Maladies_Symptomes" VALUES (2,7);
INSERT INTO "Maladies_Symptomes" VALUES (2,8);
INSERT INTO "Maladies_Symptomes" VALUES (2,2);
INSERT INTO "Maladies_Symptomes" VALUES (3,2);
INSERT INTO "Maladies_Symptomes" VALUES (3,9);
INSERT INTO "Maladies_Symptomes" VALUES (3,8);
INSERT INTO "Maladies_Symptomes" VALUES (3,10);
INSERT INTO "Questions_Symptomes" VALUES (13,5);
INSERT INTO "Questions_Symptomes" VALUES (14,5);
INSERT INTO "Questions_Symptomes" VALUES (15,5);
INSERT INTO "Questions_Symptomes" VALUES (16,5);
INSERT INTO "Questions_Symptomes" VALUES (9,4);
INSERT INTO "Questions_Symptomes" VALUES (10,4);
INSERT INTO "Questions_Symptomes" VALUES (11,4);
INSERT INTO "Questions_Symptomes" VALUES (12,4);
INSERT INTO "Questions_Symptomes" VALUES (5,3);
INSERT INTO "Questions_Symptomes" VALUES (6,3);
INSERT INTO "Questions_Symptomes" VALUES (7,3);
INSERT INTO "Questions_Symptomes" VALUES (8,3);
INSERT INTO "Questions_Symptomes" VALUES (1,2);
INSERT INTO "Questions_Symptomes" VALUES (2,2);
INSERT INTO "Questions_Symptomes" VALUES (3,2);
INSERT INTO "Questions_Symptomes" VALUES (4,2);
INSERT INTO "Questions_Symptomes" VALUES (17,6);
INSERT INTO "Questions_Symptomes" VALUES (18,6);
INSERT INTO "Questions_Symptomes" VALUES (19,6);
INSERT INTO "Questions_Symptomes" VALUES (20,6);
INSERT INTO "Questions_Symptomes" VALUES (21,7);
INSERT INTO "Questions_Symptomes" VALUES (22,7);
INSERT INTO "Questions_Symptomes" VALUES (23,7);
INSERT INTO "Questions_Symptomes" VALUES (24,7);
INSERT INTO "Questions_Symptomes" VALUES (25,8);
INSERT INTO "Questions_Symptomes" VALUES (26,8);
INSERT INTO "Questions_Symptomes" VALUES (27,8);
INSERT INTO "Questions_Symptomes" VALUES (28,8);
INSERT INTO "Questions_Symptomes" VALUES (29,9);
INSERT INTO "Questions_Symptomes" VALUES (30,9);
INSERT INTO "Questions_Symptomes" VALUES (31,9);
INSERT INTO "Questions_Symptomes" VALUES (32,9);
INSERT INTO "Questions_Symptomes" VALUES (33,10);
INSERT INTO "Questions_Symptomes" VALUES (34,10);
INSERT INTO "Questions_Symptomes" VALUES (35,10);
INSERT INTO "Questions_Symptomes" VALUES (36,10);
INSERT INTO "Reponses" VALUES (1,'Je n''ai pas l''impression *halète* d''avoir beaucoup *halète* d''essoufflements *halète* ',3);
INSERT INTO "Reponses" VALUES (2,'Cela m''arrive *halète*  quelques fois.',2);
INSERT INTO "Reponses" VALUES (3,'Oui cela m''arrive souvent ',1);
INSERT INTO "Reponses" VALUES (4,'quelque petit boss il a just un peu grossi quoi',3);
INSERT INTO "Reponses" VALUES (5,'il a prit un peu de volume ',2);
INSERT INTO "Reponses" VALUES (6,'Vous avez raison docteur MON SEIN EST ENNNNNNOOOOOOORME',1);
INSERT INTO "Reponses" VALUES (7,'des fois j''ai peut être une ou 2 douleurs mais ca ma pas l''air important ',3);
INSERT INTO "Reponses" VALUES (8,'oui j''ai des douleurs dans mon corp des fois ',2);
INSERT INTO "Reponses" VALUES (9,'oui  chaque jour  j''ai l''impression que mon corp passe dans la moulinette',1);
INSERT INTO "Reponses" VALUES (10,'*Crache une flaque de sang sur la table* Pas du tout docteur.',3);
INSERT INTO "Reponses" VALUES (11,'quelque gout de temps en temps mais c''est tranquil"',2);
INSERT INTO "Reponses" VALUES (12,'oui quand je tousse je tousse beaucoup de sang c''est une des raison pour lequel je suis venus',1);
INSERT INTO "Reponses" VALUES (13,'Che n''est pas choif. *parle avec la langue seche *',3);
INSERT INTO "Reponses" VALUES (14,'Oui j''ai soif',2);
INSERT INTO "Reponses" VALUES (15,'donné moi de l''eau s''il vous plaiiiit',1);
INSERT INTO "Reponses" VALUES (16,'Non docteur *s''urine dessus*',3);
INSERT INTO "Reponses" VALUES (17,'C''est vrai que la dernière fois je me suis uriné dessus et j''avais pas remarqué',2);
INSERT INTO "Reponses" VALUES (18,'Oui monsieur, j''ai de plus en plus envie d''aller au toilettes"',1);
INSERT INTO "Reponses" VALUES (19,'Je vois juste un peu moin bien',3);
INSERT INTO "Reponses" VALUES (20,'Je vois flou de temps en temps',2);
INSERT INTO "Reponses" VALUES (21,'Docteur!!! Pourquoi avez vous éteint la lumière?',1);
INSERT INTO "Reponses" VALUES (22,'oui il se peut que j''ai quelque douleur',3);
INSERT INTO "Reponses" VALUES (23,'oui c''est temps si j''ai l''impression il y a un camion qui m''a rouler sur la tête',2);
INSERT INTO "Reponses" VALUES (24,'DOCTEUR J''ai mal a la tête , j''ai mal a ma tête AAAAAAAAAAAAAAAAAAAAH!!!!!!!!',1);
INSERT INTO "Reponses" VALUES (25,'*une goutte de sang sort de son nez* … Non.',3);
INSERT INTO "Reponses" VALUES (26,'Pas beaucoup, cela m''arrive seulement 2 à 3 fois par heure',2);
INSERT INTO "Reponses" VALUES (27,'Oui je saigne beaucoup du nez ces temps ci ',1);
INSERT INTO "Reponses_Symptomes" VALUES (2,2);
INSERT INTO "Reponses_Symptomes" VALUES (2,3);
INSERT INTO "Reponses_Symptomes" VALUES (2,1);
INSERT INTO "Reponses_Symptomes" VALUES (3,4);
INSERT INTO "Reponses_Symptomes" VALUES (3,5);
INSERT INTO "Reponses_Symptomes" VALUES (3,6);
INSERT INTO "Reponses_Symptomes" VALUES (4,7);
INSERT INTO "Reponses_Symptomes" VALUES (4,8);
INSERT INTO "Reponses_Symptomes" VALUES (4,9);
INSERT INTO "Reponses_Symptomes" VALUES (5,10);
INSERT INTO "Reponses_Symptomes" VALUES (5,11);
INSERT INTO "Reponses_Symptomes" VALUES (5,12);
INSERT INTO "Reponses_Symptomes" VALUES (6,13);
INSERT INTO "Reponses_Symptomes" VALUES (6,14);
INSERT INTO "Reponses_Symptomes" VALUES (6,15);
INSERT INTO "Reponses_Symptomes" VALUES (7,16);
INSERT INTO "Reponses_Symptomes" VALUES (7,17);
INSERT INTO "Reponses_Symptomes" VALUES (7,18);
INSERT INTO "Reponses_Symptomes" VALUES (8,19);
INSERT INTO "Reponses_Symptomes" VALUES (8,20);
INSERT INTO "Reponses_Symptomes" VALUES (8,21);
INSERT INTO "Reponses_Symptomes" VALUES (9,22);
INSERT INTO "Reponses_Symptomes" VALUES (9,23);
INSERT INTO "Reponses_Symptomes" VALUES (9,24);
INSERT INTO "Reponses_Symptomes" VALUES (10,25);
INSERT INTO "Reponses_Symptomes" VALUES (10,26);
INSERT INTO "Reponses_Symptomes" VALUES (10,27);
INSERT INTO "Questions" VALUES (1,1,'Avez-vous remarqué des essoufflements récemment?',130,125);
INSERT INTO "Questions" VALUES (2,1,'Avez-vous remarqué des essoufflements ? Je suis là pour vous aider.',70,110);
INSERT INTO "Questions" VALUES (3,1,'Franchement, vous avez des problèmes de souffle ou vous exagérez simplement?',180,150);
INSERT INTO "Questions" VALUES (4,1,'Avez vous Des essoufflements ?C''est la chose la plus banale que j''ai entendue aujourd''hui. Mais bon, parlons-en, je suppose.',180,150);
INSERT INTO "Questions" VALUES (5,1,'Avez-vous observé un gonflement inhabituel dans l''un de vos seins?',130,125);
INSERT INTO "Questions" VALUES (6,1,'Il peut être délicat d''en parler, mais avez-vous remarqué des changements dans la taille ou la forme de vos seins récemment?',70,110);
INSERT INTO "Questions" VALUES (7,1,'Évidemment, vous n''avez pas remarqué quelque chose d''aussi évident qu''un gonflement anormal dans votre sein, n''est-ce pas?',180,150);
INSERT INTO "Questions" VALUES (8,1,'Vous seriez au courant si un de vos seins était étrangement gros, non?Certains ne remarquent même pas les choses les plus évidentes.',180,150);
INSERT INTO "Questions" VALUES (9,1,'Ressentez-vous des douleurs dans différentes parties de votre corps sans cause apparente?',130,125);
INSERT INTO "Questions" VALUES (10,1,'Je m''inquiète de savoir si vous avez éprouvé des douleurs inexpliquées dans diverses parties de votre corps. Pourriez-vous m''en dire plus?',70,110);
INSERT INTO "Questions" VALUES (11,1,'Percevez-vous des sensations douloureuses de nature diffuse et inexpliquée, touchant diverses régions de votre anatomie?',180,150);
INSERT INTO "Questions" VALUES (12,1,'Avez vous des Douleurs sans cause apparente? Certaines personnes sont vraiment douillettes',180,150);
INSERT INTO "Questions" VALUES (13,1,'Avez-vous remarqué la présence de sang dans votre crachat lorsque vous toussez?',130,125);
INSERT INTO "Questions" VALUES (14,1,'Je m''inquiète de votre bien-être. Avez-vous récemment remarqué du sang lorsque vous toussez? C''est important pour que je puisse mieux comprendre votre situation.',70,110);
INSERT INTO "Questions" VALUES (15,1,'Vous crachez du sang quand vous toussez? C''est grave ou vous essayez juste d''attirer l''attention?',180,150);
INSERT INTO "Questions" VALUES (16,1,'Avez-vous constaté l''émission de crachats teintés d''hémoptysie lors de vos épisodes de toux?',180,150);
INSERT INTO "Questions" VALUES (17,1,'Avez-vous remarqué une soif intense récemment? Cela pourrait être un signe important.',130,125);
INSERT INTO "Questions" VALUES (18,1,'Je m''inquiète pour votre bien-être. Avez-vous ressenti une soif intense ces derniers temps? Prenez votre temps pour me répondre, je suis là pour vous aider.',70,110);
INSERT INTO "Questions" VALUES (19,1,'Avez-vous observé une polydipsie significative au cours des dernières semaines, se manifestant par une sensation persistante de soif intense et inextinguible?',180,150);
INSERT INTO "Questions" VALUES (20,1,'Vous avez l''air assoiffé, non? Certaine personne n''ont pas l''eau courant , penser aux petits africain.',180,150);
INSERT INTO "Questions" VALUES (21,1,'Avez-vous noté une augmentation inhabituelle de la fréquence pour aller urine',130,125);
INSERT INTO "Questions" VALUES (22,1,'Je m''inquiète pour votre confort. Avez-vous noté une augmentation inhabituelle de la fréquence des mictions? Prenez votre temps pour répondre, je suis là pour vous soutenir.',70,110);
INSERT INTO "Questions" VALUES (23,1,'Avez-vous constaté une augmentation atypique de la fréquence des mictions, caractérisée par une nécessité impérieuse d''uriner à des intervalles rapprochés et dépassant la normale',180,150);
INSERT INTO "Questions" VALUES (24,1,'Vous n''auriez pas remarqué que vous courez aux toilettes comme un robinet qui fuit, par hasard? Ou bien, vous attendez que cela tourne en véritable inondation pour m''en parler?',180,150);
INSERT INTO "Questions" VALUES (25,1,' Rencontrez-vous des problèmes tels que la vision floue ou des changements soudains de vision récemment? Cela pourrait nécessiter une attention particulière."',130,125);
INSERT INTO "Questions" VALUES (26,1,'Je m''inquiète pour votre bien-être visuel. Avez-vous remarqué des épisodes de vision floue ou des changements soudains dans votre vision? Prenez votre temps pour répondre, je suis là pour vous aider',70,110);
INSERT INTO "Questions" VALUES (27,1,'Rencontrez-vous des problèmes tels que la vision floue ou des changements soudains de vision?C''est vraiment difficile de comprendre comment quelqu''un peut négliger quelque chose d''aussi évident',180,150);
INSERT INTO "Questions" VALUES (28,1,'Avez-vous constaté des altérations visuelles telles que la vision floue persistante ou des modifications soudaines de l''acuité visuelle au cours des derniers jours?',180,150);
INSERT INTO "Questions" VALUES (29,1,'Souffrez-vous de maux de tête fréquents et persistants? Il pourrait être utile d''explorer cette question plus en détail.',130,125);
INSERT INTO "Questions" VALUES (30,1,'je m''inquiète pour votre bien-être. Avez-vous été aux prises avec des maux de tête fréquents et persistants récemment? Prenez votre temps pour me répondre, je suis là pour vous soutenir',70,110);
INSERT INTO "Questions" VALUES (31,1,'Vous avez l''air d''avoir de vos maux de tête. C''est peut-être juste votre tête qui ne peut pas supporter votre propre présence.',180,150);
INSERT INTO "Questions" VALUES (32,1,'Avez-vous éprouvé une fréquence élevée et une persistance dans la survenue de céphalées au cours de la période récente?',180,150);
INSERT INTO "Questions" VALUES (33,1,'Avez-vous des saignements de nez fréquents sans raison apparente? ',130,125);
INSERT INTO "Questions" VALUES (34,1,'Je m''inquiète pour votre bien-être. Avez-vous remarqué des saignements de nez fréquents sans raison apparente récemment? Prenez votre temps pour répondre, je suis là pour vous',70,110);
INSERT INTO "Questions" VALUES (35,1,'Le sang qui coule de votre nez, c''est votre idée de la décoration personnelle? Ou vous avez simplement décidé que mes journées n''étaient pas assez occupées sans avoir à nettoyer votre hémorragie nasale ici?',180,150);
INSERT INTO "Questions" VALUES (36,1,'Avez-vous constaté une incidence fréquente de saignements de nez sans étiologie évidente au cours des derniers jours',180,150);
INSERT INTO "ImagesPatient" VALUES (1,'personnage1','personnage1_triste',NULL,NULL,NULL);
INSERT INTO "ImagesPatient" VALUES (2,'personnage2',NULL,NULL,NULL,NULL);
INSERT INTO "ImagesPatient" VALUES (3,'personnage3',NULL,NULL,NULL,NULL);
INSERT INTO "ImagesPatient" VALUES (4,'personnage4',NULL,NULL,NULL,NULL);
INSERT INTO "ImagesPatient" VALUES (5,'personnage5',NULL,NULL,NULL,NULL);
INSERT INTO "Patients" VALUES (1,'toadJ',10,0,1);
INSERT INTO "Patients" VALUES (2,'mario',20,0,2);
INSERT INTO "Patients" VALUES (3,'luigi',30,0,3);
INSERT INTO "Patients" VALUES (4,'ToadB',50,0,4);
INSERT INTO "Patients" VALUES (5,'peach',25,0,5);
COMMIT;
