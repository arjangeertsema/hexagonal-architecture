# Set-up
find . -type d -name ".generated" | xargs rm -rf
find . -type d -name "Generated" | xargs rm -rf
mkdir Generated

docker run --rm --user "$(id -u):$(id -g)" -v "${PWD}:/local" openapitools/openapi-generator-cli generate \
    -i https://raw.githubusercontent.com/synionnl/website/${VERSION}/docs/blog/bizdevops/living-documentation/product.openapi.yaml \
    -g aspnetcore \
    -o /local/.generated \
    --additional-properties=aspnetCoreVersion=5.0,buildTarget=library,operationIsAsync=true,operationResultTask=true,packageName=Adapters.Rest.Generated,swashbuckleVersion=5.0.0

docker run --rm --user "$(id -u):$(id -g)" -v "${PWD}:/local" openapitools/openapi-generator-cli generate \
    -i https://raw.githubusercontent.com/synionnl/website/${VERSION}/docs/blog/bizdevops/living-documentation/answer-question.openapi.yaml \
    -g aspnetcore \
    -o /local/.generated \
    --additional-properties=aspnetCoreVersion=5.0,buildTarget=library,operationIsAsync=true,operationResultTask=true,packageName=Adapters.Rest.Generated,swashbuckleVersion=5.0.0

docker run --rm --user "$(id -u):$(id -g)" -v "${PWD}:/local" openapitools/openapi-generator-cli generate \
    -i https://raw.githubusercontent.com/synionnl/website/${VERSION}/docs/blog/bizdevops/living-documentation/review-answer.openapi.yaml \
    -g aspnetcore \
    -o /local/.generated \
    --additional-properties=aspnetCoreVersion=5.0,buildTarget=library,operationIsAsync=true,operationResultTask=true,packageName=Adapters.Rest.Generated,swashbuckleVersion=5.0.0

docker run --rm --user "$(id -u):$(id -g)" -v "${PWD}:/local" openapitools/openapi-generator-cli generate \
    -i https://raw.githubusercontent.com/synionnl/website/${VERSION}/docs/blog/bizdevops/living-documentation/modify-answer.openapi.yaml.yaml \
    -g aspnetcore \
    -o /local/.generated \
    --additional-properties=aspnetCoreVersion=5.0,buildTarget=library,operationIsAsync=true,operationResultTask=true,packageName=Adapters.Rest.Generated,swashbuckleVersion=5.0.0

# Copy generated .cs files
cd .generated/src/Adapters.Rest.Generated
find -type f -name '*.cs' | 
    while read FILE; do cp --parents "$FILE" ../../../Generated; done

# # Clean up
cd ../../..
rm -rf .generated