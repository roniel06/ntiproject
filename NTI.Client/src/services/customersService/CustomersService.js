import { BaseService } from "../baseService/baseService"

export class CustomersService extends BaseService {
    constructor() {
        super('/customers')
    }

    getCustomersWithExpensiveItems = async () => {
        return await this.get("expensive-items");
    }
}