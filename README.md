# 🎾 TennisPlayersStats
Cette application est une API REST simple développée en C# avec ASP.NET Core
qui permet de récupérer des informations sur des joueurs de tennis et leurs statistiques.

📦 Prérequis
Avant de lancer l'application, assurez-vous d'avoir installé les outils suivants sur votre machine :
- .NET 8.0 SDK ou supérieur : Télécharger le SDK .NET https://dotnet.microsoft.com/download

🚀 Lancer l'Application en Local
- Télécharger la release sous forme d'archive zip https://github.com/AdrienTODA/TennisPlayersStats/releases
- Décompresser l'archive dans un dossier que vous pourrez retrouver
- Lancer TennisPlayersStats.exe

🧪 Tester l'API
- Utilisez les url suivantes :
	- https://localhost:5000/Players/getplayersbyrank
	- https://localhost:5000/Players/52 (utilisez un id disponible dans le fichier json)
	- https://localhost:5000/Players/getstats

📝 Endpoints Disponibles

Méthode : GET | Endpoint : /Players/getplayersbyrank | Description : Retourne la liste des joueurs triés par rang.

Méthode : GET | Endpoint : /Players/{id} | Description : Retourne les informations d'un joueur par ID.

Méthode : GET | Endpoint :/Players/getstats | Description : Retourne les statistiques globales.
