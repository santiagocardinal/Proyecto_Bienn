using System.Threading.Tasks;
using Discord.Commands;

namespace Library;

public class SellerCommand
{
    public class SearchSellerByIdCommand : ModuleBase<SocketCommandContext>
    {
        [Command("searchSellerById")]
        [Summary("Busca un vendedor por ID.")]
        public async Task ExecuteAsync(string id)
        {
            string result = Facade.SearchSeller_ById(id);
            await ReplyAsync(result);
        }
    }

    public class GetPendingResponsesCommand : ModuleBase<SocketCommandContext>
    {
    }
}