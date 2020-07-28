using DevFramework.Core.CrossCuttingConcerns.Security.Web;
using DevFramework.Core.DataAccess.SqlStoredProcedure;
using QueryBook.DataAccess.Abstract;
using QueryBook.Entites.ComplexType;
using QueryBook.Entites.Concrete.Users;
using QueryBook.Entites.StoredProcedure.SpHelpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QueryBook.DataAccess.Concrete.StoredProcedure
{
    public class SpUserDal : IUserDal
    {
        public User Add(User entity)
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            AuthenticationHelper.Disponse();
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public User Get(User filter)
        {
            var parameters = new List<DbQueryParameter>()
            {
                new DbQueryParameter("InsCode",filter.InsCode),
                new DbQueryParameter("Email",filter.Email)
            };
            return LoadDataSetUser(SqlDbManager.Instance().ExecuteProcedureDataSets(UserSpHelper.GetSpName(), parameters, SqlDbManager.SqlConnectionMode.QUERYBOOK));
        }

        public User Get(Expression<Func<User, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<User> GetList(User filter)
        {
            throw new NotImplementedException();
        }

        public List<UserRoleItem> GetUserRoles(User user)
        {
            var parameters = new List<DbQueryParameter>()
            {
                new DbQueryParameter("InsCode",user.InsCode),
                new DbQueryParameter("UserId",user.Id)
            };
            List<UserRoleItem> userRoleItems = new List<UserRoleItem>();
            var result = SqlDbManager.Instance().ExecuteProcedureDataSets(UserSpHelper.GetUserRolesSpName(), parameters, SqlDbManager.SqlConnectionMode.QUERYBOOK);
            if(result!=null)
            {
                if(result.Tables.Count>0&&result.Tables[0].Rows.Count>0)
                {
                    foreach (DataRow item in result.Tables[0].Rows)
                    {
                        UserRoleItem userRoleItem = new UserRoleItem { RoleName = item["RoleName"].ToString() };
                        userRoleItems.Add(userRoleItem);
                    }
                }
            }
            return userRoleItems;
        }

        public User Update(User entity)
        {
            throw new NotImplementedException();
        }

        private User LoadDataSetUser(DataSet ds)
        {
            if(ds!=null)
            {
                if(ds.Tables.Count>0&&ds.Tables[0].Rows.Count>0)
                {
                    User user = new User
                    {
                        InsCode = ds.Tables[0].Rows[0]["InsCode"].ToString(),
                        Id = (int)ds.Tables[0].Rows[0]["Id"],
                        Name = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Name"].ToString()) ? "" : ds.Tables[0].Rows[0]["Name"].ToString(),
                        UserName = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["UserName"].ToString()) ? "" : ds.Tables[0].Rows[0]["UserName"].ToString(),
                        Password = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Password"].ToString()) ? "" : ds.Tables[0].Rows[0]["Password"].ToString(),
                        UserGroupId = (int)ds.Tables[0].Rows[0]["UserGroupId"],
                        Email = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Email"].ToString()) ? "" : ds.Tables[0].Rows[0]["Email"].ToString(),
                        Title = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Title"].ToString()) ? "" : ds.Tables[0].Rows[0]["Title"].ToString(),
                        Explanation = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Explanation"].ToString()) ? "" : ds.Tables[0].Rows[0]["Explanation"].ToString(),
                        UserGroupName = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["UserGroupName"].ToString()) ? "" : ds.Tables[0].Rows[0]["UserGroupName"].ToString(),
                        LastName = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["LastName"].ToString()) ? "" : ds.Tables[0].Rows[0]["LastName"].ToString()
                    };
                    return user;
                }
            }
            return null;
        }
    }
}
