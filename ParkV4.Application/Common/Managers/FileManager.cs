using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace ParkV4.Application.Common.Managers
{
    public class FileManager
{
    private readonly IHostingEnvironment _hostingEnvironment;

    public FileManager(IHostingEnvironment hostingEnvironment)
    {
        _hostingEnvironment = hostingEnvironment;
    }

    public void Delete(string mainUrl)
    {
        var path = Path.Combine(_hostingEnvironment.ContentRootPath, mainUrl);
        
        if(File.Exists(path))
        {
            File.Delete(path);
        }
    }

    public string Upload(IFormFile? file, ImagePath root)
    {
        string uploadFolder = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", root.ToString());

        bool exists = Directory.Exists(uploadFolder);

        if (!exists)
            Directory.CreateDirectory(uploadFolder);

        if (file is not { Length: > 0 })
        {
            switch (root)
            {
                case ImagePath.UserProfilePhoto:
                    return ImagePath.UserProfilePhoto + "/default_user.jpeg";
                case ImagePath.CompanyPhoto:
                    return ImagePath.CompanyPhoto + "/default_company.jpeg";
            }
        }

        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName).ToLower();
        var path = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", root.ToString(), fileName);
        using (var stream = new FileStream(path, FileMode.Create))
        {
            file.CopyTo(stream);
        }

        var url = Path.Combine(root.ToString(), fileName);
        return url;
    }
}
}