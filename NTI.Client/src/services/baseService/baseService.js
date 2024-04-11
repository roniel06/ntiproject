import baseHttpRequest from "./baseHttpRequest";

export const BaseService = class {
    constructor(controller) {
        this.controller = controller;
    }
    async getAll() {
        return await baseHttpRequest.get(this.controller);
    }
    async get(id) {
        return await baseHttpRequest.get(`${this.controller}/${id}`);
    }
    async create(data) {
        return await baseHttpRequest.post(this.controller, data);
    }
    async update(id, data) {
        return await baseHttpRequest.put(`${this.controller}/${id}`, data);
    }
    async delete(id) {
        return await baseHttpRequest.delete(`${this.controller}/${id}`);
    }
}