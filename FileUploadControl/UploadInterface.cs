using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace FileUploadControl
{
    public interface UploadInterface
    {

        void UploadFileMultiple(List<IFormFile> files);
    }
}
