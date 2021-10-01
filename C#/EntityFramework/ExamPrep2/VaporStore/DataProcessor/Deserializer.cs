namespace VaporStore.DataProcessor
{
	using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using VaporStore.Data.Models;
    using VaporStore.DataProcessor.Dto.Import;

    public static class Deserializer
	{
		public static string ImportGames(VaporStoreDbContext context, string jsonString)
		{
			var output = new StringBuilder();
			var games = JsonConvert.DeserializeObject<IEnumerable<GameJsonImportModel>>(jsonString);

            foreach (var jsonGame in games)
            {
                if (!IsValid(jsonGame) || !jsonGame.Tags.Any())
                {
                    output.AppendLine("Invalid Data");
					continue;
                }

				var genre = context.Genres.FirstOrDefault(x => x.Name == jsonGame.Genre)
					?? new Genre { Name = jsonGame.Genre };

				var developer = context.Developers.FirstOrDefault(x => x.Name == jsonGame.Developer)
					?? new Developer { Name = jsonGame.Developer };

				var game = new Game
				{
					Name = jsonGame.Name,
					Genre = genre,
					Developer = developer,
					Price = jsonGame.Price,
					ReleaseDate = jsonGame.ReleaseDate.Value
				};

                foreach (var jsonTag in jsonGame.Tags)
                {
					var tag = context.Tags.FirstOrDefault(x => x.Name == jsonTag)
						?? new Tag { Name = jsonTag };

					game.GameTags.Add(new GameTag { Tag = tag });
                }

				output.AppendLine($"Added {game.Name} ({game.Genre.Name}) with {game.GameTags.Count} tags");
				context.Games.Add(game);
				context.SaveChanges();
			}

			
			return output.ToString().TrimEnd();
		}

		public static string ImportUsers(VaporStoreDbContext context, string jsonString)
		{
			var output = new StringBuilder();
			var users = JsonConvert.DeserializeObject<IEnumerable<UserJsonInputModel>>(jsonString);

            foreach (var jsonUser in users)
            {
				if (!IsValid(jsonUser) || !jsonUser.Cards.All(IsValid))
                {
					output.AppendLine("Invalid Data");
					continue;
				}

				var user = new User
				{
					Age = jsonUser.Age,
					Email = jsonUser.Email,
					FullName = jsonUser.FullName,
					Username = jsonUser.Username,
					Cards = jsonUser.Cards.Select(c => new Card
					{
						Cvc = c.CVC,
						Number = c.Number,
						Type = c.Type.Value
					}).ToList(),
				};

				output.AppendLine($"Imported {user.Username} with {user.Cards.Count} cards");

				context.Users.Add(user);
				context.SaveChanges();
            }

			return output.ToString().TrimEnd();
		}

		public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
		{
			var output = new StringBuilder();

			XmlSerializer serializer =
				new XmlSerializer(typeof(PurchaseXmlInputModel[]), new XmlRootAttribute("Purchases"));

			var purchases = (PurchaseXmlInputModel[])serializer
				.Deserialize(new StringReader(xmlString));

            foreach (var xmlPurchase in purchases)
            {
                if (!IsValid(xmlPurchase))
                {
					output.AppendLine("Invalid Data");
					continue;
                }

				var parsedDate = DateTime.TryParseExact(
						xmlPurchase.Date, "dd/MM/yyyy HH:mm",
						CultureInfo.InvariantCulture, DateTimeStyles.None, out var date);

                if (!parsedDate)
                {
					output.AppendLine("Invalid Data");
					continue;
				}

				var purchase = new Purchase
				{
					ProductKey = xmlPurchase.Key,
					Type = xmlPurchase.Type.Value,
					Date = date
				};

				purchase.Card = context.Cards.FirstOrDefault(x => x.Number == xmlPurchase.Card);
				purchase.Game = context.Games.FirstOrDefault(x => x.Name == xmlPurchase.Title);

				context.Purchases.Add(purchase);

				output.AppendLine($"Imported {xmlPurchase.Title} for {purchase.Card.User.Username}");
            }

			return output.ToString().TrimEnd();
		}

		private static bool IsValid(object dto)
		{
			var validationContext = new ValidationContext(dto);
			var validationResult = new List<ValidationResult>();

			return Validator.TryValidateObject(dto, validationContext, validationResult, true);
		}
	}
}