namespace DataLayer;
public static class OptionSeed
{
	public static List<Option> All =>
	[
	new()
		{
			Id = 1,
			OptionKey = "AdminMobile",
			OptionValue = "09301724389",
			CreatedAt = new DateTime(2025, 1, 1, 12, 0, 0),
			UpdatedAt = new DateTime(2025, 1, 1, 12, 0, 0),
			Slug = "AdminMobile"
		},
		 new()
		{
			Id = 2,
			OptionKey = "Telegram",
			OptionValue = "https://t.me/your_username",
			CreatedAt = new DateTime(2025, 1, 1, 12, 0, 0),
			UpdatedAt = new DateTime(2025, 1, 1, 12, 0, 0),
			Slug = "Telegram"
		},
		 new()
		{
			Id = 3,
			OptionKey = "Whatsapp",
			OptionValue = "https://wa.me/989123456789",
			CreatedAt = new DateTime(2025, 1, 1, 12, 0, 0),
			UpdatedAt = new DateTime(2025, 1, 1, 12, 0, 0),
			Slug = "Whatsapp"
		},
		 new()
		{
			Id = 4,
			OptionKey = "Instagram",
			OptionValue = "https://www.instagram.com/your_username",
			CreatedAt = new DateTime(2025, 1, 1, 12, 0, 0),
			UpdatedAt = new DateTime(2025, 1, 1, 12, 0, 0),
			Slug = "Instagram"
		},
		 new()
		{
			Id = 5,
			OptionKey = "MinBulkOrder",
			OptionValue = "5000000",
			CreatedAt = new DateTime(2025, 1, 1, 12, 0, 0),
			UpdatedAt = new DateTime(2025, 1, 1, 12, 0, 0),
			Slug = "MinBulkOrder"
		},
		new()
		{
			Id = 6,
			OptionKey = "MinCountOfProduct",
			OptionValue = "5",
			CreatedAt = new DateTime(2025, 1, 1, 12, 0, 0),
			UpdatedAt = new DateTime(2025, 1, 1, 12, 0, 0),
			Slug = "MinCountOfProduct"
		}
	];
}
