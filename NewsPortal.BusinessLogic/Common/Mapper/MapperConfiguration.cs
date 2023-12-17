namespace NewsPortal.BusinessLogic.Common.Mapper
{
    /// <summary>
    ///     Provides a named configuration for maps. Naming conventions become scoped per profile.
    ///     
    ///     Когда вы создаете свой класс, наследующий от Profile, вы можете определить различные 
    ///     маппинги между свойствами объектов. Это позволяет вам явно указывать, какие свойства 
    ///     из одного объекта должны быть сопоставлены с какими свойствами в другом объекте.
    ///     
    ///     Польза от использования Profile заключается в том, что вы можете группировать 
    ///     правила маппинга для различных типов данных в отдельные классы, что делает код более 
    ///     организованным и легко поддерживаемым. Также это может быть полезно, если у вас есть 
    ///     несколько профилей маппинга для разных частей вашего приложения.
    /// </summary>
    
    public class MapperConfiguration : Profile
    {
        public MapperConfiguration()
        {
            AutoMapperFactory();
        }

        private void AutoMapperFactory()
        {
            UserModelMapping();
            RoleModelMapping();
        }

        private void UserModelMapping()
        {
            CreateMap<UserModel, User>();
            CreateMap<User, UserModel>();
        }

        private void RoleModelMapping() 
        {
            CreateMap<RoleModel, Role>();
            CreateMap<Role, RoleModel>();
        }
    }
}
