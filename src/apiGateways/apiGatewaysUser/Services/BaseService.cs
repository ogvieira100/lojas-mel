using buildingBlocksCore.Utils;
using System.Text.Json;
using System.Text;
using buildingBlocksCore.Models.Response;

namespace apiGatewaysUser.Services
{
    public abstract class BaseService
    {
        protected readonly LNotifications _notification;

        protected BaseService(LNotifications notification)
        {
            _notification = notification ?? new LNotifications();

        }

        protected StringContent GetContentJsonUTF8(object dado)
        {
            return new StringContent(
                JsonSerializer.Serialize(dado),
                Encoding.UTF8,
                "application/json");
        }
        protected async Task TreatErrorsResponse<T>(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {

                var responseApi = await DeserializeObjResponse<BaseResponseApi<T>>(response);
                _notification.AddRange(responseApi.Errors);
            }
        }


        protected async Task<T> DeserializeObjResponse<T>(HttpResponseMessage responseMessage)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var cont = await responseMessage.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(cont, options);
        }
    
    }
}
