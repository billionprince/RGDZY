using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.ComponentModel;
using System;

namespace RGDZY.control
{
/*    [Table(Name = "User")]
    public class User
    {
        [Column(IsPrimaryKey = true, Name = "Name", UpdateCheck = UpdateCheck.Never)]
        public string Name { get; set; }

        [Column(Name = "StudentId", UpdateCheck = UpdateCheck.Never)]
        public string StudentId { get; set; }

        [Column(Name = "Authority", UpdateCheck = UpdateCheck.Never)]
        public int Authority { get; set; }

        [Column(Name = "Password", UpdateCheck = UpdateCheck.Never)]
        public string Password { get; set; }

        [Column(Name = "Introduction", UpdateCheck = UpdateCheck.Never)]
        public string Introduction { get; set; }

        [Column(Name = "Link", UpdateCheck = UpdateCheck.Never)]
        public string Link { get; set; }

        [Column(Name = "Hometown", UpdateCheck = UpdateCheck.Never)]
        public string Hometown { get; set; }

        [Column(Name = "Birthday", UpdateCheck = UpdateCheck.Never)]
        public DateTime Birthday { get; set; }

        [Column(Name = "University", UpdateCheck = UpdateCheck.Never)]
        public string University { get; set; }

        [Column(Name = "Email", UpdateCheck = UpdateCheck.Never)]
        public string Email { get; set; }

        [Column(Name = "Phone", UpdateCheck = UpdateCheck.Never)]
        public string Phone { get; set; }

        [Column(Name = "RealName", UpdateCheck = UpdateCheck.Never)]
        public string RealName { get; set; }

        public User()
        {
        }
    }

    static public class UserAuthority
    {
        enum AFlag : byte { F1 = 0, F2, F3, F4, F5 };
        static bool hasAFlag(uint authority, AFlag flag)
        {
            int offset = (int)flag;
            uint mask = (uint)(0x1 << offset);
            if (((uint)authority & mask) != 0x0)
                return true;
            else
                return false;
        }
    }*/

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.User")]
    [global::System.Runtime.Serialization.DataContractAttribute()]
    public partial class User : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private string _Id;

        private string _Name;

        private string _StudentId;

        private int _Authority;

        private string _Password;

        private EntitySet<DeviceUse> _DeviceUse;

        private bool serializing;

        #region 可扩展性方法定义
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(string value);
        partial void OnIdChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        partial void OnStudentIdChanging(string value);
        partial void OnStudentIdChanged();
        partial void OnAuthorityChanging(int value);
        partial void OnAuthorityChanged();
        partial void OnPasswordChanging(string value);
        partial void OnPasswordChanged();
        #endregion

        public User()
        {
            this.Initialize();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", DbType = "NVarChar(50) NOT NULL", CanBeNull = false, IsPrimaryKey = true)]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 1)]
        public string Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this.OnIdChanging(value);
                    this.SendPropertyChanging();
                    this._Id = value;
                    this.SendPropertyChanged("Id");
                    this.OnIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "NVarChar(MAX)", UpdateCheck = UpdateCheck.Never)]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 2)]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                if ((this._Name != value))
                {
                    this.OnNameChanging(value);
                    this.SendPropertyChanging();
                    this._Name = value;
                    this.SendPropertyChanged("Name");
                    this.OnNameChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_StudentId", DbType = "NVarChar(MAX)", UpdateCheck = UpdateCheck.Never)]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 3)]
        public string StudentId
        {
            get
            {
                return this._StudentId;
            }
            set
            {
                if ((this._StudentId != value))
                {
                    this.OnStudentIdChanging(value);
                    this.SendPropertyChanging();
                    this._StudentId = value;
                    this.SendPropertyChanged("StudentId");
                    this.OnStudentIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Authority", DbType = "Int NOT NULL")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 4)]
        public int Authority
        {
            get
            {
                return this._Authority;
            }
            set
            {
                if ((this._Authority != value))
                {
                    this.OnAuthorityChanging(value);
                    this.SendPropertyChanging();
                    this._Authority = value;
                    this.SendPropertyChanged("Authority");
                    this.OnAuthorityChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Password", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 5)]
        public string Password
        {
            get
            {
                return this._Password;
            }
            set
            {
                if ((this._Password != value))
                {
                    this.OnPasswordChanging(value);
                    this.SendPropertyChanging();
                    this._Password = value;
                    this.SendPropertyChanged("Password");
                    this.OnPasswordChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "FK_DeviceUse_User", Storage = "_DeviceUse", ThisKey = "Id", OtherKey = "UserId", DeleteRule = "NO ACTION")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 6, EmitDefaultValue = false)]
        public EntitySet<DeviceUse> DeviceUse
        {
            get
            {
                if ((this.serializing
                            && (this._DeviceUse.HasLoadedOrAssignedValues == false)))
                {
                    return null;
                }
                return this._DeviceUse;
            }
            set
            {
                this._DeviceUse.Assign(value);
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
            if ((this.PropertyChanging != null))
            {
                this.PropertyChanging(this, emptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void attach_DeviceUse(DeviceUse entity)
        {
            this.SendPropertyChanging();
            entity.User = this;
        }

        private void detach_DeviceUse(DeviceUse entity)
        {
            this.SendPropertyChanging();
            entity.User = null;
        }

        private void Initialize()
        {
            this._DeviceUse = new EntitySet<DeviceUse>(new Action<DeviceUse>(this.attach_DeviceUse), new Action<DeviceUse>(this.detach_DeviceUse));
            OnCreated();
        }

        [global::System.Runtime.Serialization.OnDeserializingAttribute()]
        [global::System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        public void OnDeserializing(StreamingContext context)
        {
            this.Initialize();
        }

        [global::System.Runtime.Serialization.OnSerializingAttribute()]
        [global::System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        public void OnSerializing(StreamingContext context)
        {
            this.serializing = true;
        }

        [global::System.Runtime.Serialization.OnSerializedAttribute()]
        [global::System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        public void OnSerialized(StreamingContext context)
        {
            this.serializing = false;
        }
    }

}
