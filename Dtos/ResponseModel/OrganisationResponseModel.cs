namespace HNGSTAGETWO.Dtos.ResponseModel
{
    public class OrganisationResponseModel :BaseResponse
    {
        public OrganisationDto? Data { get; set; }
    }

    public class OrganisationsResponseModel : BaseResponse
    {
        public List<OrganisationDto>? Data { get; set; }
    }
}
