// See https://aka.ms/new-console-template for more information
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

/// <summary>
/// Обрабатывает сообщения телеграм бота: https://t.me/Get1_Chat1_Info1_Bot
/// </summary>
internal sealed class TelegramBackgroundService
{
    private readonly ITelegramBotClient _bot;

    public TelegramBackgroundService()
    {
        _bot = new TelegramBotClient("6106307773:AAEH8ebQ30ky63LfemQftvbRL6qhg6mqYCU");
    }


    /// <summary>
    /// Отлавливает ошибки чат бота
    /// </summary>
    /// <param name="botClient">Клиентский интерфейс для использования Telegram Bot API</param>
    /// <param name="exception"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine(exception.Message);

        error = true;
        await Task.Delay(30_000, cancellationToken);
        error = false;
    }

    private bool error = false;

    private static readonly string[] s = [
"Призрак крадет тыкву джека",
"Вампир тайно пьёт кровь на вечеринки",
"Ведьма мешает заклинание в книги",
"Мумия сбегает из музея",
"Зомби ворует увлажняющий крем для кожи.",
"Ленин восстал из мертвых и баллотируется в президенты.",
"Некромант открыл бизнес по поднятию из мертвых",
"Мужчина женится на трупе невесты.",
"Джек Потрошитель раздает конфеты в публичном доме.",
"Лавкрафт напился и призвал Ктулху",
"Сусанин проводит экскурсию на болоте",
"Илон Маск случайно уронил пизанскую башню.",
"Ящеры выкручивают лампочки в парадной древних русов.",
"Фреди Крюгиру приснился кошмар",
"Сатана пришел на прием в бюджетную поликлинику",
"Девочка из звонка попала на шоу «беремена в 16»",
"Дима Маслеников реально увидел призрака.",
"Чужой устроился в Роскосмос",
"Вили Вонка открывает табачный завод.",
        ];

    /// <summary>
    /// Обработка действий пользователя
    /// </summary>
    /// <param name="botClient">Клиентский интерфейс для использования Telegram Bot API</param>
    /// <param name="update">Этот объект представляет входящее обновление</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (error)
            return;
        switch (update.Type)
        {
            case Telegram.Bot.Types.Enums.UpdateType.Unknown:
                break;
            case Telegram.Bot.Types.Enums.UpdateType.Message:
                if (update.Message!.EntityValues?.FirstOrDefault(v => v == "/new") is { })
                {
                    await botClient.SendTextMessageAsync(update.Message!.Chat, Random.Shared.GetItems(s, 1)[0], cancellationToken: cancellationToken);
                }
                break;
            case Telegram.Bot.Types.Enums.UpdateType.InlineQuery:
                break;
            case Telegram.Bot.Types.Enums.UpdateType.ChosenInlineResult:
                break;
            case Telegram.Bot.Types.Enums.UpdateType.CallbackQuery:
                break;
            case Telegram.Bot.Types.Enums.UpdateType.EditedMessage:
                break;
            case Telegram.Bot.Types.Enums.UpdateType.ChannelPost:
                break;
            case Telegram.Bot.Types.Enums.UpdateType.EditedChannelPost:
                break;
            case Telegram.Bot.Types.Enums.UpdateType.ShippingQuery:
                break;
            case Telegram.Bot.Types.Enums.UpdateType.PreCheckoutQuery:
                break;
            case Telegram.Bot.Types.Enums.UpdateType.Poll:
                break;
            case Telegram.Bot.Types.Enums.UpdateType.PollAnswer:
                break;
            case Telegram.Bot.Types.Enums.UpdateType.MyChatMember:
                break;
            case Telegram.Bot.Types.Enums.UpdateType.ChatMember:
                break;
            case Telegram.Bot.Types.Enums.UpdateType.ChatJoinRequest:
                break;
            default:
                break;
        }

    }

    public async void ExecuteAsync(CancellationToken stoppingToken)
    {
        ReceiverOptions receiverOptions = new();

        _bot.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            receiverOptions,
            stoppingToken
        );

        await _bot.SetMyCommandsAsync([
            new(){
                Command="new",
                Description="Получить задание"

            }], cancellationToken: stoppingToken);
    }
}