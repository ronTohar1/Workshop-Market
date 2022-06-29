import ClientResponse from "./Response"

export async function fetchResponse<T>(responsePromise: Promise<ClientResponse<T>>) {
  try {
    const serverResponse = await responsePromise
    if (serverResponse.errorOccured) {
      return Promise.reject(serverResponse.errorMessage)
    }
    return serverResponse.value
  }
  catch (e) {
    return Promise.reject("An error occured trying to send your request")
  }
}

export async function convergePromises<T>(promises: Promise<T>[]): Promise<T[]> {
  return promises.reduce(
    async (promisesSum: Promise<T[]>, currVal: Promise<T>) => {
      return (await promisesSum).concat([(await currVal)])
    }
    , Promise.resolve([])
  )
}
