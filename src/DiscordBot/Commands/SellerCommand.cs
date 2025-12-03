using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace Library;

public class SellerCommand : ModuleBase<SocketCommandContext>
{
    
    ///<summary>
    ///Comando para registrar una seller, solo se genera un nuevo seller si el usuario que lo ejecuta es administrador.
    /// Uso: !createSeller S1 Raul raul@mail.com 099333666
    ///</summary>
    [Command("createSeller")]
    [Summary("Crea un nuevo vendedor (solo administradores).")]
    public async Task CreateSellerCommand(
        string id,
        string name,
        string mail,
        string phone)
    {
       //Esto es lo que verifica si el admin corresponde al admin del servidor.
       
        var author = Context.User as SocketGuildUser;

        if (author == null)
        {
            await ReplyAsync("Error interno: no se pudo determinar el usuario.");
            return;
        }

        if (!UserIsAdmin(author))
        {
            await ReplyAsync("No tienes permisos. Solo administradores pueden crear vendedores.");
            return;
        }

        string result = Facade.CreateSeller(id, name, mail, phone);
        await ReplyAsync(result);
    }

    /// <summary>
    /// Verifica que el usuario tenga permisos de administrador del servidor.
    /// </summary>
    private bool UserIsAdmin(SocketGuildUser user)
    {
        return user.GuildPermissions.Administrator;
    }


    ///<summary>
    ///Comando para buscar un seller por su id.
    /// Uso: !searchSellerById S1
    ///</summary>
    [Command("searchSellerById")]
    [Summary("Busca un vendedor por ID.")]
    public async Task SearchSellerByIdCommand(string id)
    {
        string result = Facade.SearchSeller_ById(id);
        await ReplyAsync(result);
    }


    ///<summary>
    ///Comando para suspender un seller por su id, solo lo puede ejecutar el usuario administrador del servidor.
    /// Uso: !suspendSeller S1
    ///</summary>
    [Command("suspendSeller")]
    [Summary("Suspende un vendedor por ID (solo administradores).")]
    public async Task SuspendSellerCommand(string sellerId)
    {
        var user = Context.User as SocketGuildUser;

        if (user == null || !user.GuildPermissions.Administrator)
        {
            await ReplyAsync("No tienes permisos para suspender vendedores.");
            return;
        }

        string result = Facade.SuspendSeller(sellerId);
        await ReplyAsync(result);
    }


    /// <summary>
    /// Comando para eliminar sellers, lo puede ejecutar unicamente un seller administrador del servidor
    /// Uso: !deleteSeller S1
    /// </summary>
    [Command("deleteSeller")]
    [Summary("Elimina un vendedor por ID (solo administradores).")]
    public async Task DeleteSellerCommand(string sellerId)
    {
        var user = Context.User as SocketGuildUser;

        if (user == null || !user.GuildPermissions.Administrator)
        {
            await ReplyAsync(" No tienes permisos para eliminar vendedores.");
            return;
        }

        string result = Facade.DeleteSeller(sellerId);
        await ReplyAsync(result);
    }


    [Command("enableSeller")]
    [Summary("Habilita un vendedor por ID (solo administradores).")]
    public async Task EnableSellerCommand(string sellerId)
    {
        var user = Context.User as SocketGuildUser;

        if (user == null || !user.GuildPermissions.Administrator)
        {
            await ReplyAsync("No tienes permisos para habilitar vendedores.");
            return;
        }

        string result = Facade.EnableSeller(sellerId);
        await ReplyAsync(result);
    }
   
    //DEFENSA LUCIA RODRIGUEZ, COMANDO DISCORD :)
    [Command("getTopSellerBonus")]
    [Summary("Muestra el vendedor con más ventas y su bono correspondiente.")]
    public async Task GetTopSellerBonusCommand()
    {
        string result = Facade.GetTopSellerBonus();
        await ReplyAsync(result);
    }
}


