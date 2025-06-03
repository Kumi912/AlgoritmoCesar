using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;

namespace Desencryptar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DecryptionController : ControllerBase
    {
        [HttpPost]
        [Route("cesar")]
        public IActionResult DecryptMessage([FromBody] DecryptionRequest request)
        {
            if (string.IsNullOrEmpty(request.EncryptedMessage))
                return BadRequest("El mensaje no puede estar vacío.");

                    string decryptedMessage = DecryptCesar(request.EncryptedMessage, request.Shift);
                    return Ok(new { OriginalMessage = decryptedMessage });
        }

        private string DecryptCesar(string encryptedMessage, int shift)
        {
           char[] decryptedChars = encryptedMessage.ToCharArray();

            for (int i = 0; i < decryptedChars.Length; i++)
            {
                char c = decryptedChars[i];

                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';
                    decryptedChars[i] = (char)(((c - offset - shift + 26) % 26) + offset);
                }
            }

            return new string(decryptedChars);
        }
    }

    public class DecryptionRequest
    {
        public string EncryptedMessage { get; set; }
        public int Shift { get; set; }
    }
}