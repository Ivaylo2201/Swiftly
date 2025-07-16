using PersistenceService.Entities;

namespace PersistenceService.Seed;

public class Data
{
    protected static readonly List<BankOperationEntity> BankOperations =
    [
        new() { BankOperationCode = "CRED" },
        new() { BankOperationCode = "SPAY" },
        new() { BankOperationCode = "SSTD" },
        new() { BankOperationCode = "SLEV" },
        new() { BankOperationCode = "CLRG" },
        new() { BankOperationCode = "INTC" },
        new() { BankOperationCode = "FXDR" },
        new() { BankOperationCode = "NETS" },
        new() { BankOperationCode = "BOOK" },
    ];
    
    protected static readonly List<ChargeEntity> Charges =
    [
        new() { ChargeCode = "BEN" },
        new() { ChargeCode = "OUR" },
        new() { ChargeCode = "SHA" },
    ];
    
    protected static readonly List<CurrencyEntity> Currencies = 
    [
        new() { CurrencyCode = "USD" },
        new() { CurrencyCode = "EUR" },
        new() { CurrencyCode = "JPY" },
        new() { CurrencyCode = "GBP" },
        new() { CurrencyCode = "AUD" },
        new() { CurrencyCode = "CAD" },
        new() { CurrencyCode = "CHF" },
        new() { CurrencyCode = "CNY" },
        new() { CurrencyCode = "SEK" },
        new() { CurrencyCode = "NZD" },
        new() { CurrencyCode = "MXN" },
        new() { CurrencyCode = "SGD" },
        new() { CurrencyCode = "HKD" },
        new() { CurrencyCode = "NOK" },
        new() { CurrencyCode = "KRW" },
        new() { CurrencyCode = "TRY" },
        new() { CurrencyCode = "RUB" },
        new() { CurrencyCode = "INR" },
        new() { CurrencyCode = "BRL" },
        new() { CurrencyCode = "ZAR" },
        new() { CurrencyCode = "DKK" },
        new() { CurrencyCode = "PLN" },
        new() { CurrencyCode = "TWD" },
        new() { CurrencyCode = "THB" },
        new() { CurrencyCode = "BGN" }
    ];

    protected static readonly List<CountryEntity> Countries =
    [
        new() { CountryName = "United States" },
        new() { CountryName = "China" },
        new() { CountryName = "India" },
        new() { CountryName = "Japan" },
        new() { CountryName = "Germany" },
        new() { CountryName = "United Kingdom" },
        new() { CountryName = "France" },
        new() { CountryName = "Brazil" },
        new() { CountryName = "Italy" },
        new() { CountryName = "Canada" },
        new() { CountryName = "Russia" },
        new() { CountryName = "South Korea" },
        new() { CountryName = "Australia" },
        new() { CountryName = "Spain" },
        new() { CountryName = "Mexico" },
        new() { CountryName = "Indonesia" },
        new() { CountryName = "Netherlands" },
        new() { CountryName = "Saudi Arabia" },
        new() { CountryName = "Turkey" },
        new() { CountryName = "Switzerland" }
    ];

    protected static readonly List<CityEntity> Cities =
    [
        new() { CityName = "New York" },
        new() { CityName = "Los Angeles" },
        new() { CityName = "Chicago" },

        new() { CityName = "Shanghai" },
        new() { CityName = "Beijing" },
        new() { CityName = "Guangzhou" },

        new() { CityName = "Mumbai" },
        new() { CityName = "Delhi" },
        new() { CityName = "Bangalore" },

        new() { CityName = "Tokyo" },
        new() { CityName = "Osaka" },
        new() { CityName = "Nagoya" },

        new() { CityName = "Berlin" },
        new() { CityName = "Hamburg" },
        new() { CityName = "Munich" },

        new() { CityName = "London" },
        new() { CityName = "Birmingham" },
        new() { CityName = "Manchester" },

        new() { CityName = "Paris" },
        new() { CityName = "Marseille" },
        new() { CityName = "Lyon" },

        new() { CityName = "São Paulo" },
        new() { CityName = "Rio de Janeiro" },
        new() { CityName = "Brasília" },

        new() { CityName = "Rome" },
        new() { CityName = "Milan" },
        new() { CityName = "Naples" },

        new() { CityName = "Toronto" },
        new() { CityName = "Montreal" },
        new() { CityName = "Vancouver" },

        new() { CityName = "Moscow" },
        new() { CityName = "Saint Petersburg" },
        new() { CityName = "Novosibirsk" },

        new() { CityName = "Seoul" },
        new() { CityName = "Busan" },
        new() { CityName = "Incheon" },

        new() { CityName = "Sydney" },
        new() { CityName = "Melbourne" },
        new() { CityName = "Brisbane" },

        new() { CityName = "Madrid" },
        new() { CityName = "Barcelona" },
        new() { CityName = "Valencia" },

        new() { CityName = "Mexico City" },
        new() { CityName = "Guadalajara" },
        new() { CityName = "Monterrey" },

        new() { CityName = "Jakarta" },
        new() { CityName = "Surabaya" },
        new() { CityName = "Bandung" },

        new() { CityName = "Amsterdam" },
        new() { CityName = "Rotterdam" },
        new() { CityName = "The Hague" },

        new() { CityName = "Riyadh" },
        new() { CityName = "Jeddah" },
        new() { CityName = "Mecca" },

        new() { CityName = "Istanbul" },
        new() { CityName = "Ankara" },
        new() { CityName = "Izmir" },

        new() { CityName = "Zurich" },
        new() { CityName = "Geneva" },
        new() { CityName = "Basel" }
    ];
}