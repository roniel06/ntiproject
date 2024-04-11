import { BaseService } from "../baseService/baseService";

export class CustomerItemsService extends BaseService {
    constructor() {
        super("/customeritems");
    }

}