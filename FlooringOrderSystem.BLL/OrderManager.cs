using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringOrderSystem.Models.Responses;
using FlooringOrderSystem.Models.Interfaces;
using FlooringOrderSystem.Data;

namespace FlooringOrderSystem.BLL
{
    public class OrderManager
    {
        private IOrderRepository _orderRepository;

        public OrderManager(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public OrderLookupResponse LookupOrder(int orderNumber, string date)
        {
            Validation validate = new Validation();

            OrderDateExist dateExist = new OrderDateExist();
            dateExist.IsDateFormatOk = validate.ValidFormat(date);
            dateExist.DoesOrderDateExist = _orderRepository.FindDate(date);

            OrderLookupResponse response = new OrderLookupResponse();
            response.Order = _orderRepository.LoadOrder(orderNumber);

            if (!dateExist.IsDateFormatOk)
            {
                response.Success = false;
                response.Message = "Date format is invalid";
                return response;
            }

            if (!dateExist.DoesOrderDateExist)
            {
                response.Success = false;
                response.Message = $"Date: {date} does not exist.";
            }
            else if (response.Order == null)
            {
                response.Success = false;
                response.Message = $"Order Number: {orderNumber} does not exist in file.";
            }
            else
            {
                response.Success = true;
            }

            return response;
        }

        //if date not exist > create new file, else if exist use file. OrderNumber cannot be used more than once.
        public OrderAddToListResponse AddOrder(string date, string customerName, string state,
            string productType, decimal area)
        {
            Validation validate = new Validation();

            OrderDateExist dateExist = new OrderDateExist();
            dateExist.IsDateFormatOk = validate.ValidFormat(date);
            dateExist.IsFutureDate = validate.FutureDate(date);
            dateExist.IsNameCorrect = validate.CharactersValidation(customerName);
            dateExist.IsAreaCorrect = validate.Area(area);
            dateExist.DoesOrderDateExist = _orderRepository.FindDate(date);

            OrderAddToListResponse response = new OrderAddToListResponse();
            //response.Order = _orderRepository.LoadOrder(orderNumber);

            if (!dateExist.IsDateFormatOk)
            {
                response.Success = false;
                response.Message = "Date format is invalid";
                return response;
            }

            if (!dateExist.IsFutureDate)
            {
                response.Success = false;
                response.Message = "Date is not future date";
                return response;
            }

            if (!dateExist.IsNameCorrect)
            {
                response.Success = false;
                response.Message = "Invalid characters for name";
                return response;
            }

            if (!dateExist.IsAreaCorrect)
            {
                response.Success = false;
                response.Message = "Area is not over 100.00";
                return response;
            }

            //Date not exist and no ordernumber OR date exist and no order number
            if (!dateExist.DoesOrderDateExist && response.Order == null ||
            dateExist.DoesOrderDateExist && response.Order == null)
            {
                bool confirmOrder = false;

                response.Order = _orderRepository.AddOrder(date, customerName, state,
                productType, area, confirmOrder);

                return response;
            }

            else
            {
                response.Success = false;
                response.Message = "An error occured, please contact IT.";
            }

            return response;
        }

        public AddOrderConfirmResponse ConfirmAddResponse(string date, string customerName, string state,
            string productType, decimal area)
        {
            AddOrderConfirmResponse response = new AddOrderConfirmResponse();

            bool confirmOrder = true;

            response.Success = true;

            response.Order = _orderRepository.AddOrder(date, customerName, state,
                productType, area, confirmOrder);

            if (response.Order == null)
            {
                response.Message = "The properties selected not in list";
            }

            return response;
        }

        public OrderEditResponse CheckOrderExist(int orderNumber, string date)
        {
            Validation validate = new Validation();

            OrderDateExist dateExist = new OrderDateExist();
            dateExist.IsDateFormatOk = validate.ValidFormat(date);
            dateExist.DoesOrderDateExist = _orderRepository.FindDate(date);

            OrderEditResponse response = new OrderEditResponse();
            response.Order = _orderRepository.LoadOrder(orderNumber);

            if (dateExist.IsDateFormatOk == false)
            {
                response.Message = "Order date format was invalid.";
                return response;
            }

            if (dateExist.DoesOrderDateExist && response.Order != null)
            {
                response.Success = true;
                return response;
            }
            else
            {
                response.Success = false;
                response.Message = "Order Number does not exist.";
            }

            return response;
        }

        public OrderEditResponse EditOrder(int orderNumber, string date, string customerName, string state,
                    string productType, decimal area)
        {
            Validation validate = new Validation();

            OrderDateExist dateExist = new OrderDateExist();
            dateExist.IsAreaCorrect = validate.Area(area);
            dateExist.IsNameCorrect = validate.CharactersValidation(customerName);


            OrderEditResponse response = new OrderEditResponse();

            if (!dateExist.IsAreaCorrect)
            {
                response.Success = false;
                response.Message = "Area is not over 100";
                return response;
            }

            if (!dateExist.IsNameCorrect)
            {
                response.Success = false;
                response.Message = "Invalid characters for name";
                return response;
            }

            response.Order = _orderRepository.SaveOrder(orderNumber, date, customerName, state,
                productType, area);

            response.Success = true;

            return response;
        }

        public OrderLookupResponse DeleteOrder(int orderNumber, string date)
        {
            Validation validate = new Validation();

            OrderDateExist dateExist = new OrderDateExist();
            dateExist.IsDateFormatOk = validate.ValidFormat(date);
            dateExist.DoesOrderDateExist = _orderRepository.FindDate(date);

            OrderLookupResponse response = new OrderLookupResponse();
            response.Order = _orderRepository.LoadOrder(orderNumber);

            if (dateExist.IsDateFormatOk == false)
            {
                response.Message = "Order date format was invalid.";
                return response;
            }

            if (dateExist.DoesOrderDateExist && response.Order != null)
            {
                response.Success = true;

                response.Order = _orderRepository.DeleteOrder(orderNumber, date);

                return response;
            }
            else
            {
                response.Success = false;
                response.Message = $"Order Number: {orderNumber} does not exist in file.";
            }

            return response;
        }

    }

}
