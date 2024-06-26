import { BaseService } from "../baseService/baseService";

export class CustomerItemsService extends BaseService {
    constructor() {
        super("/customeritems");
    }

    getByItemNumberRange = async (from, to) => {
        return await this.get(`report/${from}/${to}`);
    }
}