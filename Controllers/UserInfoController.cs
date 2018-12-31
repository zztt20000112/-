using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class UserInfoController : ApiController
    {
        //检查用户名是否已注册
        private ApiTools tool = new ApiTools();
        [System.Web.Http.HttpPost]
        public HttpResponseMessage CheckUserName([FromBody]string _userName)
        {
            int num = UserInfoGetCount(_userName);//查询是否存在该用户
            if (num > 0)
            {
                return tool.MsgFormat(ResponseCode.操作失败, "不可注册/用户已注册", "1 " + _userName);
            }
            else
            {
                return tool.MsgFormat(ResponseCode.成功, "可注册", "0 " + _userName);
            }
        }

        private int UserInfoGetCount(string username)
        {
            //return Convert.ToInt32(SearchValue("select count(id) from userinfo where username='" + username + "'"));
            return username == "admin" ? 1 : 0;
        }

        public class ApiTools
        {
            private string msgModel = "{{\"code\":{0},\"message\":\"{1}\",\"result\":{2}}}";
            public ApiTools()
            {
            }
            public HttpResponseMessage MsgFormat(ResponseCode code, string explanation, string result)
            {
                string r = @"^(\-|\+)?\d+(\.\d+)?$";
                string json = string.Empty;
                if (Regex.IsMatch(result, r) || result.ToLower() == "true" || result.ToLower() == "false" || result == "[]" || result.Contains('{'))
                {
                    json = string.Format(msgModel, (int)code, explanation, result);
                }
                else
                {
                    if (result.Contains('"'))
                    {
                        json = string.Format(msgModel, (int)code, explanation, result);
                    }
                    else
                    {
                        json = string.Format(msgModel, (int)code, explanation, "\"" + result + "\"");
                    }
                }
                return new HttpResponseMessage { Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json") };
            }
        }
        public enum ResponseCode
        {
            操作失败 = 00000,
            成功 = 10200,
        }
    }
}