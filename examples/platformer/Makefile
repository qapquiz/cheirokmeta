SERVER_OUT := "platformer.sh"

.PHONY: api clean help

api: build/platformer ## AUto-generate grpc go sources

build/platformer: platformer.proto
	@protoc -I ./ \
			-I ${GOPATH}/src \
			--go_out=plugins=grpc:./ \
			platformer.proto

clean: ## Remove previous builds
	@rm $(GO_API_OUT)

help: ## Display this help screen
	@grep -E '^[a-zA-Z_-]+:.*?## .*$$' $(MAKEFILE_LIST) | sort | awk 'BEGIN {FS = ":.*?## "}; {printf "\033[36m%-30s\033[0m %s\n", $$1, $$2}'
