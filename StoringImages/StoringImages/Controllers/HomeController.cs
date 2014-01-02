namespace StoringImages.Controllers
{  
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using StoringImages.Data;
    using StoringImages.Models;

    public class HomeController : Controller
    {       
        private static readonly string passwordHash = "P@@Sw0rd";
        private static readonly string saltKey = "S@LT&KEY";
        private static readonly string vIKey = "@1B2c3D4e5F6g7H8";
        private readonly StoringImagesDbContext context;

        public HomeController()
        {
            this.context = new StoringImagesDbContext();
        }

        public ActionResult Index()
        {
            return this.View();

            #region LINQTOSQL

            //using (var db = new DataClasses1DataContext())
            //{
            //    //User user = new StoringImages.User()
            //    //{
            //    //    first_name = "from VS",
            //    //    is_voted = false,
            //    //    last_name = "from VS last name",
            //    //    email = "t@g.gbg",
            //    //    password = "test"
            //    //};

            //    //db.Users.InsertOnSubmit(user);
            //    //db.SubmitChanges();

            //    //db.Users.First(x => x.user_id == 3).email = "updated email";
            //    //db.SubmitChanges();

            //    //var users = db.Users.Select(x =>
            //    //    new UserViewModel
            //    //    {
            //    //        FirstName = x.first_name,
            //    //        Count = x.Votes.Count()
            //    //    }
            //    //);

            //    //return View(users.ToList());
            //}

            #endregion
        }

        [HttpGet]
        public ActionResult Upload()
        {
            return this.View(new UploadFileModel());
        }

        [HttpPost]
        public ActionResult Upload(UploadFileModel model)
        {
            if (ModelState.IsValid)
            {
                if (model == null)
                {
                    ModelState.AddModelError("file", "No file was uploaded");
                    return this.View();
                }

                var name = model.File.FileName;
                var size = model.File.ContentLength;
                var type = model.File.ContentType;

                this.HandleUpload(model.File.InputStream, name, size, type);

                return this.RedirectToAction("index");
            }

            return this.View();
        }

        [HttpGet]
        public ActionResult Load(int? id)
        {
            if (id == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, "Invalid profile picture was provided");
            }

            var document = this.context.Documents.FirstOrDefault(x => x.Id == id);
  
            return this.File(document.Content, document.Type);
        }

        private bool HandleUpload(Stream stream, string name, int size, string type)
        {
            bool isHandled = false;

            byte[] documentBytes = new byte[stream.Length];
            stream.Read(documentBytes, 0, documentBytes.Length);

            Document document = new Document()
            {
                Content = documentBytes,
                Size = size,
                Type = type
            };

            this.context.Documents.Add(document);
            isHandled = this.context.SaveChanges() > 0;

            return isHandled;
        }

        private string GetCookieValue()
        {
            if (Request.Cookies["test4"] != null)
            {
                return this.Decrypt(Request.Cookies["test4"].Value);
            }
            else
            {
                HttpCookie cookie = new HttpCookie("test4");
                
                cookie.Value = this.Encrypt("this is from the encrypted");
                cookie.Expires = DateTime.Now.AddHours(3);
                
                this.Response.SetCookie(cookie);

                return "this is from the encrypted";
            }
        }

        private string Encrypt(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] keyBytes = new Rfc2898DeriveBytes(passwordHash, Encoding.ASCII.GetBytes(saltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(vIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }

                memoryStream.Close();
            }

            return Convert.ToBase64String(cipherTextBytes);
        }

        private string Decrypt(string encryptedText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = new Rfc2898DeriveBytes(passwordHash, Encoding.ASCII.GetBytes(saltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(vIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();

            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }
    }
}