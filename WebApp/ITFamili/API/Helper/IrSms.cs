using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmsIrRestful;

namespace Helper
{
    public class IrSms
    {
        public void SendActivationCode(string cellNumber, int code)
        {
            var token = new Token().GetToken("14285fa8b944209efc925fa8", "123qwepjf!@#nvhk4FjS9");

            var ultraFastSend = new UltraFastSend()
            {
                Mobile = Convert.ToInt64(cellNumber),
                TemplateId = 27234,
                ParameterArray = new List<UltraFastParameters>()
                {
                    new UltraFastParameters()
                    {
                        Parameter = "verifyCode" , ParameterValue = code.ToString()
                    }
                }.ToArray()

            };

            UltraFastSendRespone ultraFastSendRespone = new UltraFast().Send(token, ultraFastSend);

            if (ultraFastSendRespone.IsSuccessful)
            {

            }
        }
    }
}