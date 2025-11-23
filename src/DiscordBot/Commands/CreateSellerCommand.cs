using System;
using System.Threading.Tasks;
using Discord.Interactions;
using Library;

namespace Library;

public class CreateSellerCommand : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("create_seller", "Crea un nuevo vendedor si la contraseña es correcta.")]
    public async Task CreateSellerAsync(
        string id,
        string name,
        string mail,
        string phone,
        string password)
    {
        string result = Facade.CreateSeller(id, name, mail, phone, password);

        await RespondAsync(result, ephemeral: true);
    }
}