# Scope

# Sistem evidencije incidenata
## Technology stack
## Angular 13 (Node JS, npm..)
## C# (.net core)
## MSSQL server
## Gitlab
## Jenkins

Zadatak:
Razviti web aplikaciju za evidenciju incidenata unutar Tiac kompanije. Sama web aplikacija treba da ima mogućnost, logovanja korisnika koji može da upravlja sa incidentima (CRUD operacije, izlista sve, save, update, delete…). Aplikacija treba da sadrži mogućnost exportovanja pojedinačnih incidenata, kao i liste incidenata u neki od formata pogodnih za reportove (PDF, CSV, EXECL …). Treba da imamo implementiran sistem permisija po rolama (neke potencialne role User, Admin, Manager).
Evidencija samih incidenata, će biti moguća na više načina, to znači da neće postojati samo sama forma za unos naslova texta itd, nego će biti mogućnost uploadovanja video, audio sadržaja itd… Pored evidencije u incidenata u datim formatima, psotojaće mogućnost pregleda istih, to podrazumeva da će se video prikazivati u nekom video zapis prikazivati u nekom video playeru, audio zapis će se preslušavati u nekom audio playeru itd…
Back end aplikacija treba da bude izdeljena na miroservise, dok će util stvari biti šerovani između back end-a aplikacija u vidu .net biblioteke.
Svaki servis treba da ima sopstevnu bazu (može samo novu šemu).
Front end aplikacija treba da se sastoji iz tri dela, design, util i core dela, gde će design i util biti angular biblioteke, a core angular aplikacija koja će koristiti te dve biblioteke.
Pored samog razvoja navedenih delova, neke kritične delove aplikacije treba “pokriti” sa unit i integracinoim testovima.
Koristićemo gitlab za source code i ticket management. Jenkins ćemo upotrebiti za CI/CD pipe-lineove. Ideja je da radimo po nekom vidu SCRUM-a, gde ćemo imati jednonedeljne sprintove, daily stand-up-e, i planiranje samih sprint golova…