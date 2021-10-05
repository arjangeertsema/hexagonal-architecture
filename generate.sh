docker run --rm --user "$(id -u):$(id -g)" -v "${PWD}/Reference.Adapters:/local" openapitools/openapi-generator-cli generate \
    -i https://documentatie.synion.nl/blog/bizdevops/living-documentation/openapi.yaml \
    -g aspnetcore \
    -o /local/Generated \
    --additional-properties=aspnetCoreVersion=5.0,buildTarget=library,operationIsAsync=true,operationResultTask=true,packageName=Reference.Adapters.Generated.Rest,swashbuckleVersion=5.0.0