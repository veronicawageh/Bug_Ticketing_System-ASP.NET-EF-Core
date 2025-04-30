using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.BL;
using BugTicketingSystem.DAL;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BugTicketingSystem.BL
{
    public class AttachementManager : IAttachementManager
    {
        private readonly IunitOfWork _unitOfWork;
        public AttachementManager(IunitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }





        //  ----------------------------------------------------AddAttachement----------------------------------------------------------------------------------------------//
        public async Task<GeneralResult> AddAttachementAsync(Guid bug_id, FileUploadRequest fileRequest)
        {
            bool bug = await IsBugIdFound(bug_id);
            if (!bug)
            {
                return new GeneralResult
                {
                    Success = false,
                    Errors = new ResultError[] { new ResultError { Message = "no bug with this id" } }
                };


            }
            var file = fileRequest.File;
            #region Validation

            if (file.Length == 0)
            {
                return new GeneralResult
                {
                    Success = false,
                    Errors = new ResultError[] { new ResultError { Message = "you must uplad file"}
                }
                };

            }
            if (file.Length > 5 * 1024 * 1024)
            {

                return new GeneralResult
                {
                    Success = false,
                    Errors = new ResultError[] { new ResultError { Message = "File is too large"}

                    }
                };

            }
            var exteenstion = Path.GetExtension(file.FileName).ToLowerInvariant();
            #endregion
            var filePath = Path.Combine(
               Directory.GetCurrentDirectory(),
               "Images", $"{Guid.NewGuid()}{exteenstion}"
               );
            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
            string fileURL = $"/api/my-static-files/{Path.GetFileName(filePath)}";


            Attachement attachementToBeAdded = new Attachement
            {
                Title = file.FileName,
                URL = fileURL,
                //Path = filePath,
                BugId = bug_id

            };

            _unitOfWork.AttachementRepo.Add(attachementToBeAdded);
            var saveResult = await _unitOfWork.SaveChangesAsync();

            if (saveResult > 0)
            {
                return new GeneralResult
                {
                    Success = true,
                    Errors = null
                };
            }
            else
            {

                return new GeneralResult
                {
                    Success = false,
                    Errors = new ResultError[] { new ResultError { Message =" unable to add this Attachement"}
                }

                };




            };
        }
        public async Task<GeneralResult> RemoveAttachmentfrombug(Guid bugId, Guid AttatchmentId)
        {
            var bugExist = await _unitOfWork.BugRepo.GetByIdAsync(bugId);
            if (bugExist == null)
            {

                return new GeneralResult
                {
                    Success = false,
                    Errors = new ResultError[] { new ResultError { Message = "this bug dose not exist" }
                    }
                };
            }


            var attachmentExist = await _unitOfWork.AttachementRepo.GetByIdAsync(AttatchmentId);
            if (attachmentExist == null)
            {
                return new GeneralResult
                {
                    Success = false,
                    Errors = new ResultError[] { new ResultError { Message = "this attachment dose not exist" }
                    }
                };
            }
            _unitOfWork.AttachementRepo.Delete(attachmentExist);
            var saveResult = await _unitOfWork.SaveChangesAsync();
            if (saveResult > 0)
            {
                return new GeneralResult
                {
                    Success = true,
                    Errors = null
                };
            }
            else
            {

                return new GeneralResult
                {
                    Success = false,
                    Errors = new ResultError[] { new ResultError { Message =" unable to delete this Attachement"}
                }

                };




            };

        }
        //---------------------------------------------------------------------------------------------------------------------------------//
        private async Task<bool> IsBugIdFound(Guid bug_id)
        {
            return await _unitOfWork.BugRepo.GetByIdAsync(bug_id) != null;
        }
        //---------------------------------------------------------------------------------------------------------------------------------//


    }
}

