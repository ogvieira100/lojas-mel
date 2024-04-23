
import axios, { AxiosError } from "axios"
import {  PropsApi } from "../types/global";


const api = ({
    withAuth = true
}: PropsApi) => {
    const instance = axios.create({
        baseURL: import.meta.env.VITE_API_BASE_URL
    });

    if (withAuth) {
        instance.defaults.headers.common['Authorization'] = localStorage.getItem(import.meta.env.VITE_LOCAL_STORAGE_AUTH_KEY);
    }

    const getApiAsync = async (urls: string): Promise<any> => {
        try {
            const resp = await instance.get(urls);
            return resp;
        } catch (error) {
            const errAxios = error as AxiosError;
            return errAxios;
        }
    };

    const postApiAsync = async (urls: string, dataEnv: object) => {
        try {
            const resp = await instance.post(urls, dataEnv);
            return resp;
        } catch (error) {
            const errAxios = error as AxiosError;
            return errAxios;
        }
    };

    const putApiAsync = async (urls: string, dataEnv: object) => {
        try {
            const resp = await instance.put(urls, dataEnv);
            return resp;
        } catch (error) {
            const errAxios = error as AxiosError;
            return errAxios;
        }
    };

    const deleteApiAsync = async (urls: string) => {
        try {
            const resp = await instance.delete(urls);
            return resp;
        } catch (error) {
            const errAxios = error as AxiosError;
            return errAxios;
        }
    };

    // Remova o ponto e vírgula aqui
    return {
        getApiAsync,
        postApiAsync,
        putApiAsync,
        deleteApiAsync
    };
};

export default api;
