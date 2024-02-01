using PharmaGOBackend.Application.Products.Commands.CreateProduct;
using static PharmaGOBackend.Core.Common.Constants.ProductConstans;

namespace PharmaGOBackend.UnitTests.Helpers.CommandsHelper;

public static class RegisterProductCommandFactory
{
    public static CreateProductCommand GetDefault()
    {
        return new CreateProductCommand(
            Common.GetRandomName(),
            50,
            "Descrição teste padrão",
            Category.Health,
            "",
            Guid.NewGuid()
            );
    }

    public static CreateProductCommand GetWithoutPharmacyId()
    {
        return new CreateProductCommand(
            Common.GetRandomName(),
            50,
            "Descrição teste padrão",
            Category.Health,
            "",
            Guid.Empty
            );
    }

    public static CreateProductCommand GetWithoutName()
    {
        return new CreateProductCommand(
            "",
            50,
            "Descrição teste padrão",
            Category.Health,
            "",
            Guid.NewGuid()
            );
    }

    public static CreateProductCommand GetWithoutAmount()
    {
        return new CreateProductCommand(
            Common.GetRandomName(),
            0,
            "Descrição teste padrão",
            Category.Health,
            "",
            Guid.NewGuid()
            );
    }

    public static CreateProductCommand GetWithOver300CaracteresDescription()
    {
        return new CreateProductCommand(
            Common.GetRandomName(),
            50,
            "purus faucibus ornare suspendisse sed nisi lacus sed viverra tellus in hac habitasse platea dictumst vestibulum rhoncus est pellentesque elit ullamcorper dignissim cras tincidunt lobortis feugiat vivamus at augue eget arcu dictum varius duis at consectetur lorem donec massa sapien faucibus et molestie ac feugiat sed lectus vestibulum mattis ullamcorper velit sed ullamcorper morbi tincidunt ornare massa eget egestas purus viverra accumsan in nisl nisi scelerisque eu ultrices vitae auctor eu augue ut lectus arcu bibendum at varius vel pharetra vel turpis nunc eget lorem dolor sed viverra ipsum nunc aliquet bibendum enim facilisis gravida neque convallis a cras semper",
            Category.Health,
            "",
            Guid.NewGuid()
            );
    }
}
