docker run --rm --user "$(id -u):$(id -g)" -v "${PWD}:/local" openapitools/openapi-generator-cli generate \
    -i /local/openapi.yml \
    -g aspnetcore \
    -o /local/generated \
    --additional-properties=aspnetCoreVersion=5.0,buildTarget=library,operationIsAsync=true,operationResultTask=true,packageName=example.adapters.rest.generated

# docker run --rm --user "$(id -u):$(id -g)" -v "${PWD}:/local" openapitools/openapi-generator-cli generate \
#     -i https://documentatie.synion.nl/blog/bizdevops/living-documentation/openapi.yaml \
#     -g aspnetcore \
#     -o /local/src/generated \
#     --additional-properties=aspnetCoreVersion=5.0,buildTarget=library,operationIsAsync=true,operationResultTask=true,packageName=example.adapters.rest.generated