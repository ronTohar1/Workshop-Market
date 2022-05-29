import ClientResponse from "./Response"

export async function fetchResponse<T>(responsePromise : ClientResponse<T> , handleServerError: (msg : string) => void): Promise<T>{
    try{
      const serverResponse = await responsePromise
      if (serverResponse.errorOccured){
        handleServerError(serverResponse.errorMessage)
        return Promise.reject(serverResponse.errorMessage)
      }
      return serverResponse.value
    }
    catch(e){
      return Promise.reject("Sorry, an unexpected error occured")
    }
  }
  