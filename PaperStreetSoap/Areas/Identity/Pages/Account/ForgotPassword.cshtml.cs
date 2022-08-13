// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using ProjectBase.Core.Models;
using PaperStreetSoap.Core.Interfaces.Services;

namespace PaperStreetSoap.Areas.Identity.Pages.Account
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;

        public ForgotPasswordModel(UserManager<ApplicationUser> userManager, IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);

                // For more information on how to enable account confirmation and password reset please
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                //var callbackUrl = Url.Page(
                //    "/Account/ResetPassword",
                //    pageHandler: null,
                //    values: new { area = "Identity", code },
                //    protocol: Request.Scheme);

                var callbackUrl = $"{Request.Scheme}://{Request.Host}/reset-password/{code}";

                var html = ResetPasswordHtml(HtmlEncoder.Default.Encode(callbackUrl));
                await _emailService.Send(Input.Email, "Reset Password", html);

                return LocalRedirect("/forgot-password-confirmation");
            }

            return Page();
        }

        private string ResetPasswordHtml(string callbackUrl)
        {
            string html = @"<!DOCTYPE html>
<html lang=""en"" xmlns=""http://www.w3.org/1999/xhtml"" xmlns:v=""urn:schemas-microsoft-com:vml"" xmlns:o=""urn:schemas-microsoft-com:office:office"" style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; margin: 0 auto; padding: 0; width: 100%;"">
<head>
    <meta charset=""utf-8""> <!-- utf-8 works for most cases -->
    <meta name=""viewport"" content=""width=device-width""> <!-- Forcing initial-scale shouldn't be necessary -->
    <meta http-equiv=""X -UA-Compatible"" content=""IE=edge""> <!-- Use the latest (edge) version of IE rendering engine -->
    <meta name=""x -apple-disable-message-reformatting"">  <!-- Disable auto-scale in iOS 10 Mail entirely -->
    <title></title> <!-- The title tag shows in email notifications, like Android 4.4. -->

    <link href=""https://fonts.googleapis.com/css?family=Montserrat:200,300,400,500,600,700,800,900"" rel=""stylesheet"">
    <script src= ""https://kit.fontawesome.com/ec8b570ee5.js"" crossorigin=""anonymous"" async></script>

    <style type=""text/css"">
        @media screen and (max-width:600px) {
            h3 {
                font-size: 0.8rem !important;
            }

            h2 {
                font-size: 1rem !important;
            }

            p, .view-post-button {
                font-size: 0.7rem !important;
            }

            .footer p, .navigation a {
                font-size: 0.6rem !important;
            }

            .footer ul li {
                font-size: 0.6rem !important;
            }
        }
    </style>
</head>

<body width=""100%"" style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; background: #f1f1f1; font-family: 'Montserrat', sans-serif; font-weight: 400; font-size: 15px; line-height: 1.8; letter-spacing: 6px; color: rgba(0,0,0,.4); mso-line-height-rule: exactly; margin: 0 auto; height: 100%; width: 100%; padding: 0;"">
    <center style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; width: 100%; background-color: #f1f1f1;"" >
        <div style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; display: none; font-size: 1px; max-height: 0px; max-width: 0px; opacity: 0; overflow: hidden; mso-hide: all; font-family: sans-serif;"">
            &zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;&zwnj;&nbsp;
        </div>
        <div style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; max-width: 600px; margin: 0 auto;"" class=""email-container"">
            <!-- BEGIN BODY -->
            <table align=""center"" role=""presentation"" cellspacing=""0"" cellpadding=""0"" border=""0"" width=""100%"" style=""-ms-text-size-adjust:100%; -webkit-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-spacing: 0; border-collapse: collapse; table-layout: fixed; margin: 0 auto;"">
                <tr style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;"">
                    <td valign=""top"" style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; padding: 1em 2.5em; background: #fff; mso-table-lspace: 0pt; mso-table-rspace: 0pt;"">
                        <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-spacing: 0; border-collapse: collapse; table-layout: fixed; margin: 0 auto;"">
                            <tr style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;"" >
                                <td width=""30%"" class=""logo"" style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; text-align: left; mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" align=""left"">
                                    <a href=""https://paperstreetsoap.io"" style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; text-decoration: none; color: #f5564e;""><img src=""https://paperstreetsoap.azureedge.net/site/logo.webp"" style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; -ms-interpolation-mode: bicubic; height: 40px;"" height=""40""></a>
                                </td>
                                <td width=""70%"" class=""logo"" style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; text-align: right; mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" align=""right"">
                                    <ul class=""navigation"" style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; padding: 0;"">
                                        <li style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; list-style: none; display: inline-block; font-weight: 500; margin-left: 15px; font-size: 0.75rem; letter-spacing: 6px;""><a href=""https://paperstreetsoap.io"" style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; text-decoration: none; color: #000000;"">Home</a></li>
                                        <li style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; list-style: none; display: inline-block; font-weight: 500; margin-left: 15px; font-size: 0.75rem; letter-spacing: 6px;""><a href=""https://paperstreetsoap.io/login"" style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; text-decoration: none; color: #000000;""> Members</a></li>
                                    </ul>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr><!-- end tr -->
                <tr style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;"">
                    <td class=""bg_black email-section"" style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; background: #ffffff; padding: 2.5em; text-align: center; mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" align=""center"">
                        <div class=""heading-section heading-section-white"" style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; color: rgba(255,255,255,.8);"">
                            <div style=""color: rgba(0,0,0,.6); -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; letter-spacing: 3px;""> Please reset your password by clicking below</div>
                            <a href=" + callbackUrl + @" style=""font-weight: 400; text-decoration:none; line-height: 1.5; color: #fff; text-align: center; vertical-align: middle; cursor: pointer; user-select: none; background-color: #ED85C7; border: 1px solid transparent; padding: 0.375rem 0.75rem; font-size: 1rem; border-radius: 0px; border-radius:50px"" class=""view-post-button""> Reset</a>
                        </div>
                    </td>
                </tr><!-- end: tr -->
            </table>
            <table align=""center"" role=""presentation"" cellspacing=""0"" cellpadding=""0"" border=""0"" width=""100%"" style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-spacing: 0; border-collapse: collapse; table-layout: fixed; margin: 0 auto;"">
                <tr style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;"">
                    <td valign=""middle"" class=""bg_primary footer email-section"" style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; padding: 2.5em; background-color: #ED85C7; color: #000000; mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" bgcolor=""#ED85C7"">
                        <table style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-spacing: 0; border-collapse: collapse; table-layout: fixed; margin: 0 auto;"">
                            <tr style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;"">
                                <td valign=""top"" width=""60%"" style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; padding-top: 20px; mso-table-lspace: 0pt; mso-table-rspace: 0pt;"">
                                    <table role=""presentation"" cellspacing=""0"" cellpadding=""0"" border=""0"" width=""100%"" style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-spacing: 0; border-collapse: collapse; table-layout: fixed; margin: 0 auto;"">
                                        <tr style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;"">
                                            <td style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; text-align: left; padding-right: 10px; mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" align=""left"">
                                                <h3 class=""heading"" style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; font-family: 'Montserrat', sans-serif; margin-top: 0; font-weight: 500; letter-spacing: 6px; color: #ffffff; font-size: 1.2rem;"">Quick Links</h3>
                                                <ul style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; margin: 0; padding: 0;"">
                                                    <li style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; letter-spacing: 3px; list-style: none; margin-bottom: 10px; color: #ffffff; font-size: 0.75rem;""><a href=""https://paperstreetsoap.io/about"" style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; text-decoration: none; color: rgba(255,255,255,1);"">About</a></li>
                                                    <li style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; letter-spacing: 3px; list-style: none; margin-bottom: 10px; color: #ffffff; font-size: 0.75rem;""><a href=""https://paperstreetsoap.io/terms-and-conditions"" style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; text-decoration: none; color: rgba(255,255,255,1);"">Terms & Conditions</a></li>
                                                    <li style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; letter-spacing: 3px; list-style: none; margin-bottom: 10px; color: #ffffff; font-size: 0.75rem;""><a href=""https://paperstreetsoap.io/dislaimer"" style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; text-decoration: none; color: rgba(255,255,255,1);"">Disclaimer</a></li>
                                                </ul>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td valign=""top"" width=""40%"" style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; padding-top: 20px; mso-table-lspace: 0pt; mso-table-rspace: 0pt;"">
                                    <table role=""presentation"" cellspacing=""0"" cellpadding=""0"" border=""0"" width=""100%"" style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-spacing: 0; border-collapse: collapse; table-layout: fixed; margin: 0 auto;"">
                                        <tr style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;"">
                                            <td style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; text-align: right; padding-left: 5px; padding-right: 5px; mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" align=""left"">
                                                <h3 class=""heading"" style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; font-family: 'Montserrat', sans-serif; margin-top: 0; font-weight: 500; letter-spacing: 6px; color: #ffffff; font-size: 1.2rem;"">Socials</h3>
                                                <ul class=""social"" style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; margin: 0; padding: 0;"">
                                                    <li style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; letter-spacing: 3px; display: inline-block; list-style: none; margin-bottom: 10px; color: #ffffff; font-size: 0.75rem;""><a href=""https://twitter.com/ApeDurden"" style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; text-decoration: none; color: rgba(255,255,255,1);""><img id=""substack-logo"" style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; -ms-interpolation-mode: bicubic; height: 10px;"" src=""https://paperstreetsoap.azureedge.net/site/twitter-logo-white.webp"" alt=""Substack logo"" height=""10""></a></li>
                                                    <li style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; letter-spacing: 3px; display: inline-block; list-style: none; margin-bottom: 10px; color: #ffffff; font-size: 0.75rem;""><a href=""https://www.youtube.com/ApeDurden"" style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; text-decoration: none; color: rgba(255,255,255,1);""><img id=""substack-logo"" style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; -ms-interpolation-mode: bicubic; height: 10px;"" src=""https://paperstreetsoap.azureedge.net/site/youtube-logo-white.webp"" alt=""Substack logo"" height=""10""></a></li>
                                                    <li style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; letter-spacing: 3px; display: inline-block; list-style: none; margin-bottom: 10px; color: #ffffff; font-size: 0.75rem; padding-right:6px""><a href=""https://tylerdurden.substack.com/?utm_source=substack&utm_medium=web&utm_campaign=reader2?utm_source=%2Fsearch%2Ftyler%2520durden&utm_medium=reader2"" style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; text-decoration: none; color: rgba(255,255,255,1);""><img id=""substack-logo"" style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; -ms-interpolation-mode: bicubic; height: 10px;"" src=""https://paperstreetsoap.azureedge.net/site/substack-white.webp"" alt=""Substack logo"" height=""10""></a></li>
                                                </ul>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr><!-- end: tr -->
                <tr style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;"">
                    <td valign=""middle"" class=""bg_black footer email-section"" style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; background: #000000; padding: 2.5em; color: rgba(255,255,255,.5); mso-table-lspace: 0pt; mso-table-rspace: 0pt;"">
                        <table style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-spacing: 0; border-collapse: collapse; table-layout: fixed; margin: 0 auto;"">
                            <tr style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;"">
                                <td valign=""top"" width=""60%"" style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt;"">
                                    <table role=""presentation"" cellspacing=""0"" cellpadding=""0"" border=""0"" width=""100%"" style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-spacing: 0; border-collapse: collapse; table-layout: fixed; margin: 0 auto;"">
                                        <tr style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;"">
                                            <td style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; text-align: left; padding-right: 10px; mso-table-lspace: 0pt; mso-table-rspace: 0pt;"" align=""left"">
                                                <p style=""-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; letter-spacing: 3px; color: #ffffff; font-size: 0.75rem;"">&copy; Paper Street Soap Ltd.</p>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

        </div>
    </center>
</body>
</html>";

            return html;
        }
    }
}
