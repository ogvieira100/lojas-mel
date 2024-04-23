import  api  from "./api";

const apiUser = () => {
    const { getApiAsync } = api({ endpoint:'' });

    const loginAsync = async () => {

        await getApiAsync('')
        return ''

    }
    return
    {
        loginAsync

    }

}

export default apiUser;